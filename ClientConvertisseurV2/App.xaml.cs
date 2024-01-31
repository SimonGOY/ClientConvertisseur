// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ClientConvertisseurV2.ViewModels;
using ClientConvertisseurV2.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV2
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        public ServiceProvider Services { get; }


        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            /// <summary>
            /// Configures the services for the application.
            /// </summary>
            ServiceCollection services = new ServiceCollection();

            //ViewModels
            services.AddTransient<ConvertisseurEuroViewModel>();

            Services = services.BuildServiceProvider();
        }

        ///<summary>
        /// Gets the current app instance in use 
        ///</summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            // Create a Frame to act as the navigation context and navigate to the first page
            Frame rootFrame = new Frame();
            // Place the frame in the current Window
            this.m_window.Content = rootFrame;
            // Ensure the current window is active
            m_window.Activate();
            //  Navigate to the first page
            //rootFrame.Navigate(typeof(ConvertisseurDevisePage)); // Devise to euro
            rootFrame.Navigate(typeof(ConvertisseurEuroPage)); // Euro to devise

            MainRoot = m_window.Content as FrameworkElement;
        }

        private Window m_window;

        public static FrameworkElement MainRoot { get; private set; }
    }
}
