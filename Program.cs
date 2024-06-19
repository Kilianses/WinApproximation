// Decompiled with JetBrains decompiler
// Type: WinApproximation.Program
// Assembly: WinApproximation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 898B1F90-738A-427A-BA6E-991ED48B7AAB
// Assembly location: C:\Users\ebysh\Downloads\102200033_exe\exe\WinApproximation.exe

using System;
using System.Windows.Forms;


namespace WinApproximation
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new MainForm());
    }
  }
}
