
#if FULL
/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Microsoft Public License. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Microsoft Public License, please send an email to 
 * dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Microsoft Public License.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/

using System;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Scripting;

namespace Microsoft.Scripting.Actions { 
    #region Generated DynamicSites

    // *** BEGIN GENERATED CODE ***

    /// <summary>
    /// Dynamic site delegate type with CodeContext passed in - arity 1
    /// </summary>
    public delegate Tret DynamicSiteTarget<T0, Tret>(DynamicSite<T0, Tret> site, CodeContext context, T0 arg0) ;

    /// <summary>
    /// Dynamic site using CodeContext passed into the Invoke method - arity 1
    /// </summary>
    public class DynamicSite<T0, Tret> : DynamicSite  {
        private DynamicSiteTarget<T0, Tret> _target;
        private RuleSet<DynamicSiteTarget<T0, Tret>> _rules;

        internal DynamicSite(DynamicAction action)
            : base(action) {
            this._rules = RuleSet<DynamicSiteTarget<T0, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static DynamicSite<T0, Tret> Create(DynamicAction action) {
            return new DynamicSite<T0, Tret>(action);
        }

        public Tret Invoke(CodeContext context, T0 arg0) {
            Validate(context);
            return _target(this, context, arg0);
        }

        public Tret UpdateBindingAndInvoke(CodeContext context, T0 arg0) {
            StandardRule<DynamicSiteTarget<T0, Tret>> rule = _rules.GetRule(context, arg0);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(context);
                return _target(this, context, arg0);
            }

            return (Tret)context.LanguageContext.Binder.UpdateSiteAndExecute<DynamicSiteTarget<T0, Tret>>(context, Action, new object[] { arg0 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type using cached CodeContext - arity 1
    /// </summary>
    public delegate Tret FastDynamicSiteTarget<T0, Tret>(FastDynamicSite<T0, Tret> site, T0 arg0) ;

    /// <summary>
    /// Dynamic site using cached CodeContext - arity 1
    /// </summary>
    public class FastDynamicSite<T0, Tret> : FastDynamicSite  {
        private FastDynamicSiteTarget<T0, Tret> _target;
        private RuleSet<FastDynamicSiteTarget<T0, Tret>> _rules;

        internal FastDynamicSite(CodeContext context, DynamicAction action)
            : base(context, action) {
            this._rules = RuleSet<FastDynamicSiteTarget<T0, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static FastDynamicSite<T0, Tret> Create(CodeContext context, DynamicAction action) {
            return new FastDynamicSite<T0, Tret>(context, action);
        }

        public Tret Invoke(T0 arg0) {
            return _target(this, arg0);
        }

        public Tret UpdateBindingAndInvoke(T0 arg0) {
            StandardRule<FastDynamicSiteTarget<T0, Tret>> rule = _rules.GetRule(Context, arg0);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(Context);
                return _target(this, arg0);
            }


            return (Tret)Context.LanguageContext.Binder.UpdateSiteAndExecute<FastDynamicSiteTarget<T0, Tret>>(Context, Action, new object[] { arg0 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type with CodeContext passed in - arity 2
    /// </summary>
    public delegate Tret DynamicSiteTarget<T0, T1, Tret>(DynamicSite<T0, T1, Tret> site, CodeContext context, T0 arg0, T1 arg1) ;

    /// <summary>
    /// Dynamic site using CodeContext passed into the Invoke method - arity 2
    /// </summary>
    public class DynamicSite<T0, T1, Tret> : DynamicSite  {
        private DynamicSiteTarget<T0, T1, Tret> _target;
        private RuleSet<DynamicSiteTarget<T0, T1, Tret>> _rules;

        internal DynamicSite(DynamicAction action)
            : base(action) {
            this._rules = RuleSet<DynamicSiteTarget<T0, T1, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static DynamicSite<T0, T1, Tret> Create(DynamicAction action) {
            return new DynamicSite<T0, T1, Tret>(action);
        }

        public Tret Invoke(CodeContext context, T0 arg0, T1 arg1) {
            Validate(context);
            return _target(this, context, arg0, arg1);
        }

        public Tret UpdateBindingAndInvoke(CodeContext context, T0 arg0, T1 arg1) {
            StandardRule<DynamicSiteTarget<T0, T1, Tret>> rule = _rules.GetRule(context, arg0, arg1);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(context);
                return _target(this, context, arg0, arg1);
            }

            return (Tret)context.LanguageContext.Binder.UpdateSiteAndExecute<DynamicSiteTarget<T0, T1, Tret>>(context, Action, new object[] { arg0, arg1 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type using cached CodeContext - arity 2
    /// </summary>
    public delegate Tret FastDynamicSiteTarget<T0, T1, Tret>(FastDynamicSite<T0, T1, Tret> site, T0 arg0, T1 arg1) ;

    /// <summary>
    /// Dynamic site using cached CodeContext - arity 2
    /// </summary>
    public class FastDynamicSite<T0, T1, Tret> : FastDynamicSite  {
        private FastDynamicSiteTarget<T0, T1, Tret> _target;
        private RuleSet<FastDynamicSiteTarget<T0, T1, Tret>> _rules;

        internal FastDynamicSite(CodeContext context, DynamicAction action)
            : base(context, action) {
            this._rules = RuleSet<FastDynamicSiteTarget<T0, T1, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static FastDynamicSite<T0, T1, Tret> Create(CodeContext context, DynamicAction action) {
            return new FastDynamicSite<T0, T1, Tret>(context, action);
        }

        public Tret Invoke(T0 arg0, T1 arg1) {
            return _target(this, arg0, arg1);
        }

        public Tret UpdateBindingAndInvoke(T0 arg0, T1 arg1) {
            StandardRule<FastDynamicSiteTarget<T0, T1, Tret>> rule = _rules.GetRule(Context, arg0, arg1);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(Context);
                return _target(this, arg0, arg1);
            }


            return (Tret)Context.LanguageContext.Binder.UpdateSiteAndExecute<FastDynamicSiteTarget<T0, T1, Tret>>(Context, Action, new object[] { arg0, arg1 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type with CodeContext passed in - arity 3
    /// </summary>
    public delegate Tret DynamicSiteTarget<T0, T1, T2, Tret>(DynamicSite<T0, T1, T2, Tret> site, CodeContext context, T0 arg0, T1 arg1, T2 arg2) ;

    /// <summary>
    /// Dynamic site using CodeContext passed into the Invoke method - arity 3
    /// </summary>
    public class DynamicSite<T0, T1, T2, Tret> : DynamicSite  {
        private DynamicSiteTarget<T0, T1, T2, Tret> _target;
        private RuleSet<DynamicSiteTarget<T0, T1, T2, Tret>> _rules;

        internal DynamicSite(DynamicAction action)
            : base(action) {
            this._rules = RuleSet<DynamicSiteTarget<T0, T1, T2, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static DynamicSite<T0, T1, T2, Tret> Create(DynamicAction action) {
            return new DynamicSite<T0, T1, T2, Tret>(action);
        }

        public Tret Invoke(CodeContext context, T0 arg0, T1 arg1, T2 arg2) {
            Validate(context);
            return _target(this, context, arg0, arg1, arg2);
        }

        public Tret UpdateBindingAndInvoke(CodeContext context, T0 arg0, T1 arg1, T2 arg2) {
            StandardRule<DynamicSiteTarget<T0, T1, T2, Tret>> rule = _rules.GetRule(context, arg0, arg1, arg2);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(context);
                return _target(this, context, arg0, arg1, arg2);
            }

            return (Tret)context.LanguageContext.Binder.UpdateSiteAndExecute<DynamicSiteTarget<T0, T1, T2, Tret>>(context, Action, new object[] { arg0, arg1, arg2 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type using cached CodeContext - arity 3
    /// </summary>
    public delegate Tret FastDynamicSiteTarget<T0, T1, T2, Tret>(FastDynamicSite<T0, T1, T2, Tret> site, T0 arg0, T1 arg1, T2 arg2) ;

    /// <summary>
    /// Dynamic site using cached CodeContext - arity 3
    /// </summary>
    public class FastDynamicSite<T0, T1, T2, Tret> : FastDynamicSite  {
        private FastDynamicSiteTarget<T0, T1, T2, Tret> _target;
        private RuleSet<FastDynamicSiteTarget<T0, T1, T2, Tret>> _rules;

        internal FastDynamicSite(CodeContext context, DynamicAction action)
            : base(context, action) {
            this._rules = RuleSet<FastDynamicSiteTarget<T0, T1, T2, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static FastDynamicSite<T0, T1, T2, Tret> Create(CodeContext context, DynamicAction action) {
            return new FastDynamicSite<T0, T1, T2, Tret>(context, action);
        }

        public Tret Invoke(T0 arg0, T1 arg1, T2 arg2) {
            return _target(this, arg0, arg1, arg2);
        }

        public Tret UpdateBindingAndInvoke(T0 arg0, T1 arg1, T2 arg2) {
            StandardRule<FastDynamicSiteTarget<T0, T1, T2, Tret>> rule = _rules.GetRule(Context, arg0, arg1, arg2);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(Context);
                return _target(this, arg0, arg1, arg2);
            }


            return (Tret)Context.LanguageContext.Binder.UpdateSiteAndExecute<FastDynamicSiteTarget<T0, T1, T2, Tret>>(Context, Action, new object[] { arg0, arg1, arg2 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type with CodeContext passed in - arity 4
    /// </summary>
    public delegate Tret DynamicSiteTarget<T0, T1, T2, T3, Tret>(DynamicSite<T0, T1, T2, T3, Tret> site, CodeContext context, T0 arg0, T1 arg1, T2 arg2, T3 arg3) ;

    /// <summary>
    /// Dynamic site using CodeContext passed into the Invoke method - arity 4
    /// </summary>
    public class DynamicSite<T0, T1, T2, T3, Tret> : DynamicSite  {
        private DynamicSiteTarget<T0, T1, T2, T3, Tret> _target;
        private RuleSet<DynamicSiteTarget<T0, T1, T2, T3, Tret>> _rules;

        internal DynamicSite(DynamicAction action)
            : base(action) {
            this._rules = RuleSet<DynamicSiteTarget<T0, T1, T2, T3, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static DynamicSite<T0, T1, T2, T3, Tret> Create(DynamicAction action) {
            return new DynamicSite<T0, T1, T2, T3, Tret>(action);
        }

        public Tret Invoke(CodeContext context, T0 arg0, T1 arg1, T2 arg2, T3 arg3) {
            Validate(context);
            return _target(this, context, arg0, arg1, arg2, arg3);
        }

        public Tret UpdateBindingAndInvoke(CodeContext context, T0 arg0, T1 arg1, T2 arg2, T3 arg3) {
            StandardRule<DynamicSiteTarget<T0, T1, T2, T3, Tret>> rule = _rules.GetRule(context, arg0, arg1, arg2, arg3);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(context);
                return _target(this, context, arg0, arg1, arg2, arg3);
            }

            return (Tret)context.LanguageContext.Binder.UpdateSiteAndExecute<DynamicSiteTarget<T0, T1, T2, T3, Tret>>(context, Action, new object[] { arg0, arg1, arg2, arg3 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type using cached CodeContext - arity 4
    /// </summary>
    public delegate Tret FastDynamicSiteTarget<T0, T1, T2, T3, Tret>(FastDynamicSite<T0, T1, T2, T3, Tret> site, T0 arg0, T1 arg1, T2 arg2, T3 arg3) ;

    /// <summary>
    /// Dynamic site using cached CodeContext - arity 4
    /// </summary>
    public class FastDynamicSite<T0, T1, T2, T3, Tret> : FastDynamicSite  {
        private FastDynamicSiteTarget<T0, T1, T2, T3, Tret> _target;
        private RuleSet<FastDynamicSiteTarget<T0, T1, T2, T3, Tret>> _rules;

        internal FastDynamicSite(CodeContext context, DynamicAction action)
            : base(context, action) {
            this._rules = RuleSet<FastDynamicSiteTarget<T0, T1, T2, T3, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static FastDynamicSite<T0, T1, T2, T3, Tret> Create(CodeContext context, DynamicAction action) {
            return new FastDynamicSite<T0, T1, T2, T3, Tret>(context, action);
        }

        public Tret Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3) {
            return _target(this, arg0, arg1, arg2, arg3);
        }

        public Tret UpdateBindingAndInvoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3) {
            StandardRule<FastDynamicSiteTarget<T0, T1, T2, T3, Tret>> rule = _rules.GetRule(Context, arg0, arg1, arg2, arg3);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(Context);
                return _target(this, arg0, arg1, arg2, arg3);
            }


            return (Tret)Context.LanguageContext.Binder.UpdateSiteAndExecute<FastDynamicSiteTarget<T0, T1, T2, T3, Tret>>(Context, Action, new object[] { arg0, arg1, arg2, arg3 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type with CodeContext passed in - arity 5
    /// </summary>
    public delegate Tret DynamicSiteTarget<T0, T1, T2, T3, T4, Tret>(DynamicSite<T0, T1, T2, T3, T4, Tret> site, CodeContext context, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) ;

    /// <summary>
    /// Dynamic site using CodeContext passed into the Invoke method - arity 5
    /// </summary>
    public class DynamicSite<T0, T1, T2, T3, T4, Tret> : DynamicSite  {
        private DynamicSiteTarget<T0, T1, T2, T3, T4, Tret> _target;
        private RuleSet<DynamicSiteTarget<T0, T1, T2, T3, T4, Tret>> _rules;

        internal DynamicSite(DynamicAction action)
            : base(action) {
            this._rules = RuleSet<DynamicSiteTarget<T0, T1, T2, T3, T4, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static DynamicSite<T0, T1, T2, T3, T4, Tret> Create(DynamicAction action) {
            return new DynamicSite<T0, T1, T2, T3, T4, Tret>(action);
        }

        public Tret Invoke(CodeContext context, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            Validate(context);
            return _target(this, context, arg0, arg1, arg2, arg3, arg4);
        }

        public Tret UpdateBindingAndInvoke(CodeContext context, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            StandardRule<DynamicSiteTarget<T0, T1, T2, T3, T4, Tret>> rule = _rules.GetRule(context, arg0, arg1, arg2, arg3, arg4);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(context);
                return _target(this, context, arg0, arg1, arg2, arg3, arg4);
            }

            return (Tret)context.LanguageContext.Binder.UpdateSiteAndExecute<DynamicSiteTarget<T0, T1, T2, T3, T4, Tret>>(context, Action, new object[] { arg0, arg1, arg2, arg3, arg4 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type using cached CodeContext - arity 5
    /// </summary>
    public delegate Tret FastDynamicSiteTarget<T0, T1, T2, T3, T4, Tret>(FastDynamicSite<T0, T1, T2, T3, T4, Tret> site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) ;

    /// <summary>
    /// Dynamic site using cached CodeContext - arity 5
    /// </summary>
    public class FastDynamicSite<T0, T1, T2, T3, T4, Tret> : FastDynamicSite  {
        private FastDynamicSiteTarget<T0, T1, T2, T3, T4, Tret> _target;
        private RuleSet<FastDynamicSiteTarget<T0, T1, T2, T3, T4, Tret>> _rules;

        internal FastDynamicSite(CodeContext context, DynamicAction action)
            : base(context, action) {
            this._rules = RuleSet<FastDynamicSiteTarget<T0, T1, T2, T3, T4, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static FastDynamicSite<T0, T1, T2, T3, T4, Tret> Create(CodeContext context, DynamicAction action) {
            return new FastDynamicSite<T0, T1, T2, T3, T4, Tret>(context, action);
        }

        public Tret Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            return _target(this, arg0, arg1, arg2, arg3, arg4);
        }

        public Tret UpdateBindingAndInvoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            StandardRule<FastDynamicSiteTarget<T0, T1, T2, T3, T4, Tret>> rule = _rules.GetRule(Context, arg0, arg1, arg2, arg3, arg4);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(Context);
                return _target(this, arg0, arg1, arg2, arg3, arg4);
            }


            return (Tret)Context.LanguageContext.Binder.UpdateSiteAndExecute<FastDynamicSiteTarget<T0, T1, T2, T3, T4, Tret>>(Context, Action, new object[] { arg0, arg1, arg2, arg3, arg4 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type with CodeContext passed in - arity 6
    /// </summary>
    public delegate Tret DynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret>(DynamicSite<T0, T1, T2, T3, T4, T5, Tret> site, CodeContext context, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) ;

    /// <summary>
    /// Dynamic site using CodeContext passed into the Invoke method - arity 6
    /// </summary>
    public class DynamicSite<T0, T1, T2, T3, T4, T5, Tret> : DynamicSite  {
        private DynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret> _target;
        private RuleSet<DynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret>> _rules;

        internal DynamicSite(DynamicAction action)
            : base(action) {
            this._rules = RuleSet<DynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static DynamicSite<T0, T1, T2, T3, T4, T5, Tret> Create(DynamicAction action) {
            return new DynamicSite<T0, T1, T2, T3, T4, T5, Tret>(action);
        }

        public Tret Invoke(CodeContext context, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) {
            Validate(context);
            return _target(this, context, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public Tret UpdateBindingAndInvoke(CodeContext context, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) {
            StandardRule<DynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret>> rule = _rules.GetRule(context, arg0, arg1, arg2, arg3, arg4, arg5);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(context);
                return _target(this, context, arg0, arg1, arg2, arg3, arg4, arg5);
            }

            return (Tret)context.LanguageContext.Binder.UpdateSiteAndExecute<DynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret>>(context, Action, new object[] { arg0, arg1, arg2, arg3, arg4, arg5 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type using cached CodeContext - arity 6
    /// </summary>
    public delegate Tret FastDynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret>(FastDynamicSite<T0, T1, T2, T3, T4, T5, Tret> site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) ;

    /// <summary>
    /// Dynamic site using cached CodeContext - arity 6
    /// </summary>
    public class FastDynamicSite<T0, T1, T2, T3, T4, T5, Tret> : FastDynamicSite  {
        private FastDynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret> _target;
        private RuleSet<FastDynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret>> _rules;

        internal FastDynamicSite(CodeContext context, DynamicAction action)
            : base(context, action) {
            this._rules = RuleSet<FastDynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static FastDynamicSite<T0, T1, T2, T3, T4, T5, Tret> Create(CodeContext context, DynamicAction action) {
            return new FastDynamicSite<T0, T1, T2, T3, T4, T5, Tret>(context, action);
        }

        public Tret Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) {
            return _target(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public Tret UpdateBindingAndInvoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) {
            StandardRule<FastDynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret>> rule = _rules.GetRule(Context, arg0, arg1, arg2, arg3, arg4, arg5);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(Context);
                return _target(this, arg0, arg1, arg2, arg3, arg4, arg5);
            }


            return (Tret)Context.LanguageContext.Binder.UpdateSiteAndExecute<FastDynamicSiteTarget<T0, T1, T2, T3, T4, T5, Tret>>(Context, Action, new object[] { arg0, arg1, arg2, arg3, arg4, arg5 }, this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type with CodeContext passed in - arity variable based on Tuple size
    /// </summary>
    public delegate Tret BigDynamicSiteTarget<T0, Tret>(BigDynamicSite<T0, Tret> site, CodeContext context, T0 arg0) where T0 : Tuple;

    /// <summary>
    /// Dynamic site using CodeContext passed into the Invoke method - arity variable based on Tuple size
    /// </summary>
    public class BigDynamicSite<T0, Tret> : DynamicSite where T0 : Tuple {
        private BigDynamicSiteTarget<T0, Tret> _target;
        private RuleSet<BigDynamicSiteTarget<T0, Tret>> _rules;

        internal BigDynamicSite(DynamicAction action)
            : base(action) {
            this._rules = RuleSet<BigDynamicSiteTarget<T0, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static BigDynamicSite<T0, Tret> Create(DynamicAction action) {
            return new BigDynamicSite<T0, Tret>(action);
        }

        public Tret Invoke(CodeContext context, T0 arg0) {
            Validate(context);
            return _target(this, context, arg0);
        }

        public Tret UpdateBindingAndInvoke(CodeContext context, T0 arg0) {
            StandardRule<BigDynamicSiteTarget<T0, Tret>> rule = _rules.GetRule(context, arg0);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(context);
                return _target(this, context, arg0);
            }

            return (Tret)context.LanguageContext.Binder.UpdateSiteAndExecute<BigDynamicSiteTarget<T0, Tret>>(context, Action, Tuple.GetTupleValues(arg0), this, ref _target, ref _rules);
        }
    }

    /// <summary>
    /// Dynamic site delegate type using cached CodeContext - arity variable based on Tuple size
    /// </summary>
    public delegate Tret BigFastDynamicSiteTarget<T0, Tret>(BigFastDynamicSite<T0, Tret> site, T0 arg0) where T0 : Tuple;

    /// <summary>
    /// Dynamic site using cached CodeContext - arity variable based on Tuple size
    /// </summary>
    public class BigFastDynamicSite<T0, Tret> : FastDynamicSite where T0 : Tuple {
        private BigFastDynamicSiteTarget<T0, Tret> _target;
        private RuleSet<BigFastDynamicSiteTarget<T0, Tret>> _rules;

        internal BigFastDynamicSite(CodeContext context, DynamicAction action)
            : base(context, action) {
            this._rules = RuleSet<BigFastDynamicSiteTarget<T0, Tret>>.EmptyRules;
            this._target = this._rules.GetOrMakeTarget(null);
        }

        public static BigFastDynamicSite<T0, Tret> Create(CodeContext context, DynamicAction action) {
            return new BigFastDynamicSite<T0, Tret>(context, action);
        }

        public Tret Invoke(T0 arg0) {
            return _target(this, arg0);
        }

        public Tret UpdateBindingAndInvoke(T0 arg0) {
            StandardRule<BigFastDynamicSiteTarget<T0, Tret>> rule = _rules.GetRule(Context, arg0);
            if (rule != null) {
                // site is truly polymorphic, build the polymorphic method
                _target = _rules.GetOrMakeTarget(Context);
                return _target(this, arg0);
            }


            return (Tret)Context.LanguageContext.Binder.UpdateSiteAndExecute<BigFastDynamicSiteTarget<T0, Tret>>(Context, Action, Tuple.GetTupleValues(arg0), this, ref _target, ref _rules);
        }
    }


    // *** END GENERATED CODE ***

    #endregion
}

#endif	
