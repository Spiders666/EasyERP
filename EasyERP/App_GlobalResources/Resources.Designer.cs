﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyERP.App_GlobalResources {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EasyERP.App_GlobalResources.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Wystąpił błąd podczas zapisywania nowych danych. Należy poprawić zaistniałe błędy..
        /// </summary>
        internal static string AdminControllerCreateError {
            get {
                return ResourceManager.GetString("AdminControllerCreateError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Zapisanie nowych danych przebiegło pomyślnie..
        /// </summary>
        internal static string AdminControllerCreateSuccess {
            get {
                return ResourceManager.GetString("AdminControllerCreateSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Z niewiadomych przyczyn nowe dane nie zostały zapisane..
        /// </summary>
        internal static string AdminControllerCreateWarning {
            get {
                return ResourceManager.GetString("AdminControllerCreateWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wystąpił błąd podczas aktualizacji. Należy poprawić dane..
        /// </summary>
        internal static string AdminControllerEditError {
            get {
                return ResourceManager.GetString("AdminControllerEditError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aktualizacja danych przebiegła pomyślnie..
        /// </summary>
        internal static string AdminControllerEditSuccess {
            get {
                return ResourceManager.GetString("AdminControllerEditSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dane został zaktualizowane przez inną osobę. Należy odświeżyć stronę w celu wczytania nowych danych..
        /// </summary>
        internal static string AdminControllerEditWarning {
            get {
                return ResourceManager.GetString("AdminControllerEditWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Anulowane.
        /// </summary>
        internal static string OrderStateCanceled {
            get {
                return ResourceManager.GetString("OrderStateCanceled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nie potwierdzone.
        /// </summary>
        internal static string OrderStateNotConfirmed {
            get {
                return ResourceManager.GetString("OrderStateNotConfirmed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Oczekiwane.
        /// </summary>
        internal static string OrderStatePending {
            get {
                return ResourceManager.GetString("OrderStatePending", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wysłane.
        /// </summary>
        internal static string OrderStateSent {
            get {
                return ResourceManager.GetString("OrderStateSent", resourceCulture);
            }
        }
    }
}
