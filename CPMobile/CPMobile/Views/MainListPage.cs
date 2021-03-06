﻿using System;
using System.Collections.Generic;
using System.Globalization;
using CPMobile.ViewModels;
using CustomLayouts.Controls;
using CustomLayouts.ViewModels;
using Xamarin.Forms;

namespace CPMobile.Views
{
    public class MainListPage : ContentPage
    {
        View _tabs;

        RelativeLayout relativeLayout;
        private TabbedPageViewModel _viewModel;
        CarouselLayout.IndicatorStyleEnum _indicatorStyle;

        public MainListPage()
        {
       
            _indicatorStyle = CarouselLayout.IndicatorStyleEnum.Tabs;
            
           // viewModel = new SwitcherPageViewModel();
            _viewModel = new TabbedPageViewModel();
            BindingContext = _viewModel;
           // BindingContext = viewModel;
            //BackgroundColor = Color.Black;
            Title = "FindMe";
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, false);
            relativeLayout = new RelativeLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var pagesCarousel = CreatePagesCarousel();
            //var dots = CreatePagerIndicatorContainer();
            _tabs = CreateTabs();

            switch (pagesCarousel.IndicatorStyle)
            {
              
                case CarouselLayout.IndicatorStyleEnum.Tabs:
                    var tabsHeight = 50;
                    relativeLayout.Children.Add(_tabs,
                        Constraint.Constant(0),
                        Constraint.RelativeToParent((parent) => { return parent.Height - tabsHeight; }),
                        Constraint.RelativeToParent(parent => parent.Width),
                        Constraint.Constant(tabsHeight)
                    );

                    relativeLayout.Children.Add(pagesCarousel,
                        Constraint.RelativeToParent((parent) => { return parent.X; }),
                        Constraint.RelativeToParent((parent) => { return parent.Y; }),
                        Constraint.RelativeToParent((parent) => { return parent.Width; }),
                        Constraint.RelativeToView(_tabs, (parent, sibling) => { return parent.Height - (sibling.Height); })
                    );
                    break;
            }

                Content = relativeLayout;
        }
        CarouselLayout CreatePagesCarousel()
        {
            var carousel = new CarouselLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IndicatorStyle = _indicatorStyle,
                ItemTemplate = new DataTemplate(typeof(DynamicTemplateLayout))
            };
            carousel.SetBinding(CarouselLayout.ItemsSourceProperty, "Pages");
            carousel.SetBinding(CarouselLayout.SelectedItemProperty, "CurrentPage", BindingMode.TwoWay);

            return carousel;
        }


        //View CreatePagerIndicatorContainer()
        //{
        //    return new StackLayout
        //    {
        //        Children = { CreatePagerIndicators() }
        //    };
        //}

        //View CreatePagerIndicators()
        //{
        //    var pagerIndicator = new PagerIndicatorDots() { DotSize = 5, DotColor = Color.Black };
        //    pagerIndicator.SetBinding(PagerIndicatorDots.ItemsSourceProperty, "Pages");
        //    pagerIndicator.SetBinding(PagerIndicatorDots.SelectedItemProperty, "CurrentPage");
        //    return pagerIndicator;
        //}

        View CreateTabsContainer()
        {
            return new StackLayout
            {
                Children = { CreateTabs() }
            };
        }

        View CreateTabs()
        {
            var pagerIndicator = new PagerIndicatorTabs() { HorizontalOptions = LayoutOptions.CenterAndExpand };
            pagerIndicator.RowDefinitions.Add(new RowDefinition() { Height = 50 });
            pagerIndicator.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(1, GridUnitType.Star) });
            //pagerIndicator.SetBinding(Grid.ColumnDefinitionsProperty, "Pages", BindingMode.Default, new SpacingConverter());
            pagerIndicator.SetBinding(PagerIndicatorTabs.ItemsSourceProperty, "Pages");
            pagerIndicator.SetBinding(PagerIndicatorTabs.SelectedItemProperty, "CurrentPage");

            return pagerIndicator;
        }
    }

    public class SpacingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = value as IEnumerable<ICarouselViewModel>;

            var collection = new ColumnDefinitionCollection();
            foreach (var item in items)
            {
                collection.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            return collection;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    
}
