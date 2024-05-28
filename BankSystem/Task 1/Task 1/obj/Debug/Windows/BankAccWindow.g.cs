﻿#pragma checksum "..\..\..\Windows\BankAccWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C7A1774883872D17EC733415AF49E4ABA20844B08B4EC36E9C7B407CB9934702"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Task_1.Windows;


namespace Task_1.Windows {
    
    
    /// <summary>
    /// BankAccountWindow
    /// </summary>
    public partial class BankAccountWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Windows\BankAccWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CurrentAccValue;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Windows\BankAccWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CurrentAccDepoValue;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Windows\BankAccWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TargetAccount;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Windows\BankAccWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddMoney;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\Windows\BankAccWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OpenAccountButton;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Windows\BankAccWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseAccountButton;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\Windows\BankAccWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MoneyValue;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\Windows\BankAccWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ActiveAccount;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\Windows\BankAccWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox IsDepoOrRegural;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\Windows\BankAccWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TargetMoneyValue;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\Windows\BankAccWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TargetIsDepoOrRegural;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Task 1;component/windows/bankaccwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\BankAccWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CurrentAccValue = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.CurrentAccDepoValue = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.TargetAccount = ((System.Windows.Controls.ComboBox)(target));
            
            #line 67 "..\..\..\Windows\BankAccWindow.xaml"
            this.TargetAccount.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TargetAccount_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 80 "..\..\..\Windows\BankAccWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Send_Money_Button);
            
            #line default
            #line hidden
            return;
            case 5:
            this.AddMoney = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\..\Windows\BankAccWindow.xaml"
            this.AddMoney.Click += new System.Windows.RoutedEventHandler(this.AddMoney_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.OpenAccountButton = ((System.Windows.Controls.Button)(target));
            
            #line 94 "..\..\..\Windows\BankAccWindow.xaml"
            this.OpenAccountButton.Click += new System.Windows.RoutedEventHandler(this.OpenAcc);
            
            #line default
            #line hidden
            return;
            case 7:
            this.CloseAccountButton = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\..\Windows\BankAccWindow.xaml"
            this.CloseAccountButton.Click += new System.Windows.RoutedEventHandler(this.CloseAcc);
            
            #line default
            #line hidden
            return;
            case 8:
            this.MoneyValue = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.ActiveAccount = ((System.Windows.Controls.ComboBox)(target));
            
            #line 114 "..\..\..\Windows\BankAccWindow.xaml"
            this.ActiveAccount.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ActiveAccount_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.IsDepoOrRegural = ((System.Windows.Controls.ComboBox)(target));
            
            #line 119 "..\..\..\Windows\BankAccWindow.xaml"
            this.IsDepoOrRegural.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.IsDepoOrRegural_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.TargetMoneyValue = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.TargetIsDepoOrRegural = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
