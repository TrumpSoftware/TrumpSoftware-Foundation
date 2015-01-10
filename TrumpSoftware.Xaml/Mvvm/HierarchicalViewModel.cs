﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TrumpSoftware.Xaml.Mvvm
{
    public abstract class HierarchicalViewModel : NotificationObject
    {
        public HierarchicalViewModel Parent { get; protected internal set; }

        protected HierarchicalViewModel(HierarchicalViewModel parent = null)
        {
            Parent = parent;
        }

        protected TViewModel GetAncestor<TViewModel>()
            where TViewModel : class
        {
            if (Parent == null)
                return null;
            if (Parent is TViewModel)
                return Parent as TViewModel;
            return Parent.GetAncestor<TViewModel>();
        }

        protected virtual IEnumerable<HierarchicalViewModel> GetChildren()
        {
            yield break;
        }

        protected IEnumerable<TViewModel> GetDescendants<TViewModel>(Func<TViewModel, bool> condition = null)
            where TViewModel : HierarchicalViewModel
        {
            condition = condition ?? (x => true);
            var children = GetChildren().OfType<TViewModel>();
            foreach (var child in children)
            {
                if (condition(child))
                    yield return child;
                var descendants = child.GetDescendants(condition);
                foreach (var descendant in descendants)
                    yield return descendant;
            }
        }

        protected void ForEach<TViewModel>(Action<TViewModel> action, Func<TViewModel, bool> condition = null)
            where TViewModel : HierarchicalViewModel
        {
            var descendants = GetDescendants(condition);
            foreach (var descendant in descendants)
                action(descendant);
        }
    }
}