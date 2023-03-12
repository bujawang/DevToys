﻿using DevToys.Api;
using Microsoft.UI.Xaml.Controls;

namespace DevToys.UI.Framework.Controls.GuiTool;

public sealed partial class UIStackPresenter : ContentControl
{
    public UIStackPresenter()
    {
        this.InitializeComponent();

        Loaded += UIStackPresenter_Loaded;
        Unloaded += UIStackPresenter_Unloaded;
    }

    internal IUIStack UIStack => (IUIStack)DataContext;

    private void UIStackPresenter_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        UIStack.IsEnabledChanged += UIStack_IsEnabledChanged;
        UIStack.IsVisibleChanged += UIStack_IsVisibleChanged;
        UIStack.OrientationChanged += UIStack_OrientationChanged;
        UIStack.ChildrenChanged += UIStack_ChildrenChanged;

        IsEnabled = UIStack.IsEnabled;
        Visibility = UIStack.IsVisible ? Microsoft.UI.Xaml.Visibility.Visible : Microsoft.UI.Xaml.Visibility.Collapsed;
        StackPanel.Orientation = UIStack.Orientation == UIOrientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical;
        SetChildren();
    }

    private void UIStackPresenter_Unloaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        UIStack.IsEnabledChanged -= UIStack_IsEnabledChanged;
        UIStack.IsVisibleChanged -= UIStack_IsVisibleChanged;
        UIStack.OrientationChanged -= UIStack_OrientationChanged;
        UIStack.ChildrenChanged -= UIStack_ChildrenChanged;
        Loaded -= UIStackPresenter_Loaded;
        Unloaded -= UIStackPresenter_Unloaded;
    }

    private void UIStack_IsEnabledChanged(object? sender, EventArgs e)
    {
        IsEnabled = UIStack.IsEnabled;
    }

    private void UIStack_IsVisibleChanged(object? sender, EventArgs e)
    {
        Visibility = UIStack.IsVisible ? Microsoft.UI.Xaml.Visibility.Visible : Microsoft.UI.Xaml.Visibility.Collapsed;
    }

    private void UIStack_OrientationChanged(object? sender, EventArgs e)
    {
        StackPanel.Orientation = UIStack.Orientation == UIOrientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical;
    }

    private void UIStack_ChildrenChanged(object? sender, EventArgs e)
    {
        SetChildren();
    }

    private void SetChildren()
    {
        StackPanel.Children.Clear();
        if (UIStack.Children is not null)
        {
            for (int i = 0; i < UIStack.Children.Length; i++)
            {
                StackPanel.Children.Add(UIElementPresenter.Create(UIStack.Children[i]));
            }
        }
    }
}
