﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Slate {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Slate.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Cannot modify live form &quot;{0}&quot;.
        /// </summary>
        internal static string CannotModifyLiveForm {
            get {
                return ResourceManager.GetString("CannotModifyLiveForm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot resume draft status on live form &quot;{0}&quot;.
        /// </summary>
        internal static string CannotResumeDraftOnLiveForm {
            get {
                return ResourceManager.GetString("CannotResumeDraftOnLiveForm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot start testing on live form &quot;{0}&quot;.
        /// </summary>
        internal static string CannotTestLiveForm {
            get {
                return ResourceManager.GetString("CannotTestLiveForm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Form &quot;{0}&quot; is already a draft.
        /// </summary>
        internal static string FormAlreadyDraft {
            get {
                return ResourceManager.GetString("FormAlreadyDraft", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Form &quot;{0}&quot; is already live.
        /// </summary>
        internal static string FormAlreadyLive {
            get {
                return ResourceManager.GetString("FormAlreadyLive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Form &quot;{0}&quot; is already testing.
        /// </summary>
        internal static string FormAlreadyTesting {
            get {
                return ResourceManager.GetString("FormAlreadyTesting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown section {0} in form &quot;{0}&quot;.
        /// </summary>
        internal static string UnknownSection {
            get {
                return ResourceManager.GetString("UnknownSection", resourceCulture);
            }
        }
    }
}
