﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CG.Green.Areas.Admin.Pages.Clients.Panels {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class PkcePanel_razor {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PkcePanel_razor() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CG.Green.Areas.Admin.Pages.Clients.Panels.PkcePanel.razor", typeof(PkcePanel_razor).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Allow Plain Text PKCE.
        /// </summary>
        internal static string AllowPlainTextPkce {
            get {
                return ResourceManager.GetString("AllowPlainTextPkce", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Allow PKCE to be sent in plain text..
        /// </summary>
        internal static string AllowPlainTextPkceHelp {
            get {
                return ResourceManager.GetString("AllowPlainTextPkceHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Require PKCE.
        /// </summary>
        internal static string RequirePkce {
            get {
                return ResourceManager.GetString("RequirePkce", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Proof Key for Code Exchange.
        /// </summary>
        internal static string RequirePkceHelp {
            get {
                return ResourceManager.GetString("RequirePkceHelp", resourceCulture);
            }
        }
    }
}
