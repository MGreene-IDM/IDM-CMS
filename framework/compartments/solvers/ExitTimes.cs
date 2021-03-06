﻿/***************************************************************************************************

Copyright (c) 2018 Intellectual Ventures Property Holdings, LLC (IVPH) All rights reserved.

EMOD is licensed under the Creative Commons Attribution-Noncommercial-ShareAlike 4.0 License.
To view a copy of this license, visit https://creativecommons.org/licenses/by-nc-sa/4.0/legalcode

***************************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using compartments.emod;
using compartments.emod.utils;
using compartments.solvers.solverbase;
using distlib;
using distlib.samplers;


namespace compartments.solvers
{
    /* 
     * Computes the Exit Times (aka Hitting Times, Stopping Times) for stochastic simulations. 
     * 
     * A function must be provided in the .emodl file, the name of which is provided in the config
     * file (default name = exitTimeEvent), e.g.:  (bool exitTimeEvent (== R 87) ) will compute the 
     * time when the species R is equal to 87.  
     * 
     * The _convergenceTest flag tests for convergence to SSA with respect to _epsilon, 
     * where _epsilon \in [0,1].  _epsilon = 0 will result in an SSA run. 
     * 
     * The _testing flag outputs statistical tests (mean and variance) during the run. 
     * 
     */

    public class ExitTimes : SolverBase
    {
        protected double[]           currentRates;
        protected List<double>       lambda;
        protected List<List<double>> lambdaLists;

        private readonly double _epsilon;
        private readonly string     _eventName;
        private readonly bool       _verbose;
        private readonly bool       _testing;
        private readonly bool       _convergenceTest;
        private readonly double _efficiencyCutoff;

        private double              expectedVariance;
        private double              expectedTime;
        private int                 sampleNumber;
        private List<double>         exitTimes;
        private double numberOfGammaRNs; 
        private Predicate           targetCondition;
        private bool                eventAchieved;

        private DistributionSampler _distributionSampler;

        public ExitTimes(ModelInfo modelInfo, double duration, int repeats, int samples)
            : base(modelInfo, duration, repeats, samples, new ModelBuilder())
        {
            currentRates    = new double[model.Reactions.Count];
            lambda          = new List<double>();
            lambdaLists     = new List<List<double>>();
            exitTimes       = new List<double>(); 
            numberOfGammaRNs= 0.0;

            _distributionSampler = RandLibSampler.CreateRandLibSampler(rng);

            Configuration config = Configuration.CurrentConfiguration;

            _epsilon            = Math.Min(config.GetParameterWithDefault("et.epsilon", 0.5), 1.0);
            _eventName          = config.GetParameterWithDefault("et.eventName", "exitTimeEvent");
            _verbose            = config.GetParameterWithDefault("et.verbose", false);
            _testing            = config.GetParameterWithDefault("et.testing", false);
            _convergenceTest    = config.GetParameterWithDefault("et.convergence", false);
            _efficiencyCutoff   = config.GetParameterWithDefault("et.efficiencyCutoff", 0.2);

            targetCondition    = model.Predicates.First(s => s.Name == _eventName);

            sampleNumber        = 1;
            eventAchieved       = false;

            if (_verbose)
            {
                Console.WriteLine("\n predicate: {0} with initial evaluation: {1} \n", targetCondition.Name, targetCondition.Value);
                Console.WriteLine("\n epsilon = {0} \n", _epsilon);
            }
        }

        protected void InitializeExitTimeVariables()
        {
            // expectation and variance of time
            expectedTime       = 0.0;
            expectedVariance   = 0.0;

            lambda.Clear(); // set of propensities

            // set of partitioned propensities
            lambdaLists.Clear();
        }

        protected override void SolveOnce()
        {
            if (_convergenceTest == true)
            {
                SolveOnceForConvergenceTest();
            }
            else
            {
                SolveOnceStandard();
            }

        }

        // Convergence Test
        // puts N samples in the exitTimes vector, all of which are from the first path 
        // that meets the event condition
        protected void SolveOnceForConvergenceTest()
        {
            // for convergence, check the statistics of the first path that meets
            // the event condition, return immediately for all other runs
            if (eventAchieved == true)
                return;

            // cleanup for next sample
            InitializeExitTimeVariables();

            // time integration
            while (CurrentTime < duration)
            {
                if (targetCondition.Value == true)
                {
                    if (_verbose)
                    { Console.WriteLine("--------------Event--------------"); }

                    eventAchieved = true;
                    break;
                }

                StepOnce();
            }

            Console.WriteLine("\n Sample Number = {0} \n", sampleNumber++);
            Console.WriteLine("E[t] = {0} \n", expectedTime);
            Console.WriteLine("Sqrt(Var[t]) = {0} \n\n", Math.Sqrt(expectedVariance));

            // compute the exit time if the event occured
            if (eventAchieved == true)
            {
                int N = (int)Math.Pow(10, 6);

                PartitionLambda();
                numberOfGammaRNs = lambdaLists.Count;

                if (_epsilon != 0.0)
                {
                    for (int n = 0; n < N; ++n)
                    {
                        double exitTimeSample = SampleTime();
                        exitTimes.Add(exitTimeSample);
                    }
                }
                else
                {
                    for (int n = 0; n < N; ++n)
                    {
                        double exitTimeSample = SampleTimeExponential();
                        exitTimes.Add(exitTimeSample);
                    }
                }
            }
            // else -> nothing to do, uninterested in result

        }

        // puts one sample in the exitTimes vector for each path
        // that meets the event condition
        protected void SolveOnceStandard()
        {
            // initialization for each run 
            eventAchieved = false;

            // cleanup for next sample
            InitializeExitTimeVariables();

            // time integration
            while (CurrentTime < duration)
            {
                if (targetCondition.Value == true)
                {
                    if (_verbose)
                    { Console.WriteLine("--------------Event--------------"); }

                    eventAchieved = true;
                    break; 
                }

                StepOnce();
            }

            if (_verbose)
            {
                Console.WriteLine("\n Sample Number = {0} \n", sampleNumber++);
                Console.WriteLine("E[t] = {0} \n", expectedTime);
                Console.WriteLine("Sqrt(Var[t]) = {0} \n\n", Math.Sqrt(expectedVariance));
            }

            // compute the exit time if the event occured
            if (eventAchieved == true)
            {
                int lengthOfLambda = lambda.Count;

                PartitionLambda(); // lambda.Count = 0 after this function call

                numberOfGammaRNs += (double)(lambdaLists.Count); // update the mean number of gamma random numbers

                double rho = ((double)lambdaLists.Count) / ((double)lengthOfLambda);


                double exitTimeSample;
                if (rho <= _efficiencyCutoff)
                {
                    exitTimeSample = SampleTime();  // sample the gamma distributions
                }
                else
                {
                    exitTimeSample = SampleTimeExponential(); // sample from the exponential distributions
                }
 

                if (_verbose)
                { Console.WriteLine("\n Gamma_1 = {0} \n", exitTimeSample); }

                exitTimes.Add(exitTimeSample); // save the result

                // statistical check
                if (_testing == true)
                {
                    Console.WriteLine("\n Sample Number = {0} \n", sampleNumber++);
                    Console.WriteLine("E[t] = {0} \n", expectedTime);
                    Console.WriteLine("Sqrt(Var[t]) = {0} \n\n", Math.Sqrt(expectedVariance));
                    int N = (int)Math.Pow(10, 6);
                    double mean = ComputeMean(N);
                    double standardDeviation = ComputeStandardDeviation(mean, N);
                }
            }
            // else -> nothing to do, uninterested in result

        }

        // SSA without time
        protected override void StepOnce()
        {
            double a0 = UpdateRates();

            if (a0 > 0.0)
            {
                double r = rng.GenerateUniformCC();

                double ExpectationOfTau  = (1.0 / a0);
                CurrentTime             += ExpectationOfTau;

                expectedTime     += ExpectationOfTau;                        // E[tau] = 1 / lambda
                expectedVariance += (ExpectationOfTau * ExpectationOfTau);   // Var[tau] = 1 / lambda^2

                double threshold = r * a0;

                int mu = SelectReaction(threshold);
                FireReaction(model.Reactions[mu]);

                // add the total propensity, which will later be used to compute the exit time
                lambda.Add(a0);
            }
            else
            {
                CurrentTime = duration;
            }
        }

        protected override double CalculateProposedTau(double tauLimit)
        {
            throw new NotImplementedException("The ExitTimes solver doesn't keep track of simulation time during a realization.");
        }

        protected override void ExecuteReactions()
        {
            throw new NotImplementedException("The ExitTimes solver overrides SolverBase::StepOnce() and doesn't need ExecuteReactions().");
        }

        // ssa method
        protected double UpdateRates()
        {
            double a0 = 0.0;
            int index = 0;
            foreach (Reaction r in model.Reactions)
            {
                double av = r.Rate;
                currentRates[index++] = av;
                a0 += av;
            }

            return a0;
        }

        // ssa method
        protected int SelectReaction(double threshold)
        {
            int mu = 0;

            if (currentRates.Length != model.Reactions.Count)
                throw new ApplicationException("rates[] array size doesn't match reaction list count.");

            for (int i = 0; i < model.Reactions.Count; i++)
            {
                threshold -= currentRates[i];
                if (threshold <= 0.0)
                {
                    mu = i;
                    break;
                }
            }

            return mu;
        }

        // partitions lambda based on the value of the error control parameter epsilon
        // epsilon = 1  => relative error of 1
        // epsilon = 0  => ssa
        // epsilon default value = 0.5
        protected void PartitionLambda()
        {
            // sort in descending order
            lambda.Sort((lambda1, lambda2) => -(lambda1.CompareTo(lambda2)));

            if (_verbose)
            {
                // print out lambda
                Console.WriteLine("\n\n");
                Console.WriteLine("Lambda [1,...,{0}]: \n", lambda.Count);
                for (int i = 0; i < lambda.Count; ++i)
                { Console.WriteLine("{0} ", lambda[i]); }
                Console.WriteLine("\n\n");
            }

            while (lambda.Count > 0)
            {
                double x = lambda[0] - lambda[0] * _epsilon;

                if (_verbose)
                { Console.WriteLine("\n x: {0}\n", x); }

                // partition the list of propensities around x
                var lambdaSublist =
                     from n in lambda
                     where n >= x
                     select n;

                var newLambda =
                     from n in lambda
                     where n < x
                     select n;

                lambdaLists.Add(lambdaSublist.ToList());
                lambda = newLambda.ToList();
            }

            if (_verbose)
            {
                // print some information 
                Console.WriteLine("\n Number of Lists: {0}\n", lambdaLists.Count);
                Console.WriteLine("Elements:");
                foreach (var sublist in lambdaLists)
                {
                    Console.WriteLine("Size: {0}:  ", sublist.Count);
                    foreach (var value in sublist)
                    {
                        Console.Write(value);
                        Console.Write(' ');
                    }
                    Console.WriteLine("\n");
                }
            }
        }

        // the summation of gamma distributed random variables
        protected double SampleTime()
        {
            double tau = 0.0;

            for (int i = 0; i < lambdaLists.Count; ++i)
            {
                List<double> lambdaSublist = lambdaLists[i];
                int n = lambdaSublist.Count;

                double theta = 0.0;

                for (int j = 0; j < n; ++j)
                {
                    theta += 1.0 / lambdaSublist[j];
                }
                theta /= n;

                // Gamma distribution
                tau += theta * _distributionSampler.StandardGamma(n);
            }

            return tau;
        }

        // no additional error, used if rho (the ratio of gamma r.v.s to exponential r.v.s is large)
        protected double SampleTimeExponential()
        {
            double tau = 0.0;

            for (int i = 0; i < lambdaLists.Count; ++i)
            {
                List<double> lambdaSublist = lambdaLists[i];
                int n = lambdaSublist.Count;

                for (int j = 0; j < n; ++j)
                {
                    tau += _distributionSampler.GenerateExponential(1.0 / lambdaSublist[j]);
                }
            }

            return tau;
        }

        // used if _testing flag is true
        protected double ComputeMean(int N)
        {
            double mean = 0.0;
            for (int i = 0; i < N; ++i)
            {
                mean += SampleTime(); // gamma
            }
            mean /= ((double)N);

            Console.WriteLine("\n <Gamma>_tf = {0} \n", mean);

            return mean;
        }

        // used if _testing flag is true
        protected double ComputeStandardDeviation(double mean, int N)
        {
            double variance = 0.0;
            double standardDeviation = 0.0;
            double x;

            for (int i = 0; i < N; ++i)
            {
                x = SampleTime(); // gamma

                variance += Math.Pow(x - mean, 2.0);
            }
            variance /= ((double)N);

            standardDeviation = Math.Sqrt(variance);

            Console.WriteLine("\n Sqrt(<Gamma^2>_tf) = {0} \n", standardDeviation);

            return standardDeviation;
        }

        // outputs all of the exit times
        public override void OutputData(string prefix)
        {
            double probabilityOfSuccess = ((double)(exitTimes.Count) / (double)SamplingParams.RealizationCount);

            string filename;

            if (_convergenceTest)
            {
                filename = prefix + _epsilon.ToString() + ".txt";
            }
            else
            {
                filename = prefix + "ExitTimes" + _epsilon.ToString() + ".txt";
                numberOfGammaRNs /= ((double)(exitTimes.Count));
            }

            using (var output = new StreamWriter(filename))
            {
                output.WriteLine("FrameworkVersion,\"{0}\",\" {1}\"", VersionInfo.Version, VersionInfo.Description);
                output.WriteLine("No. of realizations: {0}", SamplingParams.RealizationCount);
                output.WriteLine("Event probability estimate: {0}" , probabilityOfSuccess);
                output.WriteLine("Mean Number of Gamma RNs: {0}", numberOfGammaRNs);
                output.WriteLine("Time samples: ");

                for (int k = 0; k < exitTimes.Count; ++k)
                {
                    output.WriteLine("{0} ", exitTimes[k]);
                }
            }
        }

        public override string ToString()
        {
            return "ExitTimes (Simple)";
        }
    }
}
