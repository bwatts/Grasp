﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Grasp.Hypermedia {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Grasp.Hypermedia.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Expecting composite list item content.
        /// </summary>
        internal static string ExpectingCompositeListItemContent {
            get {
                return ResourceManager.GetString("ExpectingCompositeListItemContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expecting definition element &apos;dd&apos; [at: {0}].
        /// </summary>
        internal static string ExpectingDefinition {
            get {
                return ResourceManager.GetString("ExpectingDefinition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expecting term element &apos;dt&apos; [at: {0}].
        /// </summary>
        internal static string ExpectingTerm {
            get {
                return ResourceManager.GetString("ExpectingTerm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expecting {0} for definition, received {1}.
        /// </summary>
        internal static string ExpectingValue {
            get {
                return ResourceManager.GetString("ExpectingValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hyperlink is a template and cannot be converted to a URI without replacing variables.
        /// </summary>
        internal static string HyperlinkIsTemplate {
            get {
                return ResourceManager.GetString("HyperlinkIsTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid GUID value: &quot;{0}&quot;.
        /// </summary>
        internal static string InvalidGuidValue {
            get {
                return ResourceManager.GetString("InvalidGuidValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This link URI does not allow variables: {0}.
        /// </summary>
        internal static string LinkUriDoesNotAllowVariables {
            get {
                return ResourceManager.GetString("LinkUriDoesNotAllowVariables", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing header link with relationship &quot;{0}&quot;.
        /// </summary>
        internal static string MissingSelfLink {
            get {
                return ResourceManager.GetString("MissingSelfLink", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to self.
        /// </summary>
        internal static string SelfRelationship {
            get {
                return ResourceManager.GetString("SelfRelationship", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stream is an invalid HTML representation.
        /// </summary>
        internal static string StreamIsInvalidHtmlRepresentation {
            get {
                return ResourceManager.GetString("StreamIsInvalidHtmlRepresentation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Term &quot;{0}&quot; does not have a definition.
        /// </summary>
        internal static string TermHasNoDefinition {
            get {
                return ResourceManager.GetString("TermHasNoDefinition", resourceCulture);
            }
        }
    }
}
