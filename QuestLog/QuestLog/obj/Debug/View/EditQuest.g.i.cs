﻿#pragma checksum "..\..\..\View\EditQuest.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "90E281C28370C5A4A7A801E6896149A540F98E1E19BFD1E8F9CF46FFCB6046F2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using QuestLog;
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


namespace QuestLog {
    
    
    /// <summary>
    /// EditQuest
    /// </summary>
    public partial class EditQuest : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\View\EditQuest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal QuestLog.EditQuestVM vm;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\View\EditQuest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtName;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\View\EditQuest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCopper;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\View\EditQuest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtExp;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\View\EditQuest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtGold;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\View\EditQuest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSilver;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\View\EditQuest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listSubquests;
        
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
            System.Uri resourceLocater = new System.Uri("/QuestLog;component/view/editquest.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\EditQuest.xaml"
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
            this.vm = ((QuestLog.EditQuestVM)(target));
            return;
            case 2:
            
            #line 43 "..\..\..\View\EditQuest.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Drag);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 45 "..\..\..\View\EditQuest.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MinimizeApplication);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 46 "..\..\..\View\EditQuest.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MaximizeApplication);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 47 "..\..\..\View\EditQuest.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExitApplication);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtCopper = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtExp = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.txtGold = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.txtSilver = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.listSubquests = ((System.Windows.Controls.ListView)(target));
            return;
            case 12:
            
            #line 98 "..\..\..\View\EditQuest.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddSubQuest);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 101 "..\..\..\View\EditQuest.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditSubQuest);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 104 "..\..\..\View\EditQuest.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveSubQuest);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 110 "..\..\..\View\EditQuest.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Confirm);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

