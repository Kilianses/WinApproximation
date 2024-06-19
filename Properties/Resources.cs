// Decompiled with JetBrains decompiler
// Type: WinApproximation.Properties.Resources
// Assembly: WinApproximation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 898B1F90-738A-427A-BA6E-991ED48B7AAB
// Assembly location: C:\Users\ebysh\Downloads\102200033_exe\exe\WinApproximation.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;


namespace WinApproximation.Properties
{
  [DebuggerNonUserCode]
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (WinApproximation.Properties.Resources.resourceMan == null)
          WinApproximation.Properties.Resources.resourceMan = new ResourceManager("WinApproximation.Properties.Resources", typeof (WinApproximation.Properties.Resources).Assembly);
        return WinApproximation.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => WinApproximation.Properties.Resources.resourceCulture;
      set => WinApproximation.Properties.Resources.resourceCulture = value;
    }
  }
}
