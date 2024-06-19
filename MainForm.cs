// Decompiled with JetBrains decompiler
// Type: WinApproximation.MainForm
// Assembly: WinApproximation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 898B1F90-738A-427A-BA6E-991ED48B7AAB
// Assembly location: C:\Users\ebysh\Downloads\102200033_exe\exe\WinApproximation.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace WinApproximation
{
  public class MainForm : Form
  {
    private int N;
    public double[,] Data = new double[2, 15]
    {
      {
            2009,2010 ,2011 , 2012, 2013 ,2014,2015 , 2016 ,2017,  2018 , 2019 , 2020 , 2021 , 2022 ,2023


      },
      {
    213.84,230.22,256.21,315.45,406.51,547.02,559.21,583.00 ,600.59 ,604.26,615.57,631.08,643.07,678.26 ,699.25




      }
    };
    private IContainer components;
    private DataGridView DgvInitialData;
    private DataGridViewTextBoxColumn Column1;
    private Label label1;
    private Label label2;
    private NumericUpDown NudRank;
    private Button BtnCalc;
    private Label label3;
    private RichTextBox RtbxApproxPolinomial;
    private Label label4;
    private Button BtnCalcPow;
    private Label label5;
    private RichTextBox RtbxPow;
    private RichTextBox RtbxExp;
    private Label label6;
    private Button BtnCalcExp;

    public MainForm()
    {
      this.InitializeComponent();
      this.InitMatrix();
    }

    private void InitMatrix()
    {
      this.DgvInitialData.Columns.Clear();
      this.N = this.Data.Length / 2;
      this.DgvInitialData.ColumnCount = this.N;
      this.DgvInitialData.RowCount = 5;
      this.DgvInitialData.Rows[0].HeaderCell.Value = (object) "Xi";
      this.DgvInitialData.Rows[1].HeaderCell.Value = (object) "Yi";
      this.DgvInitialData.Rows[2].HeaderCell.Value = (object) "PoliF(Xi)";
      this.DgvInitialData.Rows[3].HeaderCell.Value = (object) "LogF(Xi)";
      this.DgvInitialData.Rows[4].HeaderCell.Value = (object) "ExpF(Xi)";
      for (int index = 0; index < this.N; ++index)
      {
        this.DgvInitialData.Columns[index].Width = 30;
        this.DgvInitialData.Columns[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        this.DgvInitialData.Columns[index].HeaderText = (index + 1).ToString();
        this.DgvInitialData.Rows[0].Cells[index].Value = (object) string.Format("{0:N1}", (object) this.Data[0, index]);
        this.DgvInitialData.Rows[1].Cells[index].Value = (object) string.Format("{0:N1}", (object) this.Data[1, index]);
        this.DgvInitialData.Rows[2].Cells[index].Value = (object) "";
        this.DgvInitialData.Rows[2].Cells[index].ReadOnly = true;
        this.DgvInitialData.Rows[3].Cells[index].Value = (object) "";
        this.DgvInitialData.Rows[3].Cells[index].ReadOnly = true;
        this.DgvInitialData.Rows[4].Cells[index].Value = (object) "";
        this.DgvInitialData.Rows[4].Cells[index].ReadOnly = true;
      }
    }

    private void GetData()
    {
      for (int index = 0; index < this.N; ++index)
      {
        try
        {
          this.Data[0, index] = double.Parse(this.DgvInitialData.Rows[0].Cells[index].Value.ToString());
          this.Data[1, index] = double.Parse(this.DgvInitialData.Rows[1].Cells[index].Value.ToString());
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show("Ошибка введенных данных в столбце " + (object) (index + 1));
        }
      }
    }

    private double[] GetPolinomialApproximationCoeff(int m)
    {
      int num = this.Data.Length / 2;
      double[] numArray1 = new double[m + 1];
      double[] numArray2 = new double[2 * m + 1];
      double[,] a = new double[m + 1, m + 1];
      double[] b = new double[m + 1];
      for (int y = 0; y < 2 * m + 1; ++y)
      {
        numArray2[y] = 0.0;
        for (int index = 0; index < num; ++index)
          numArray2[y] += Math.Pow(this.Data[0, index], (double) y);
      }
      for (int y = 0; y < m + 1; ++y)
      {
        b[y] = 0.0;
        for (int index = 0; index < num; ++index)
          b[y] += this.Data[1, index] * Math.Pow(this.Data[0, index], (double) y);
      }
      for (int index1 = 0; index1 < m + 1; ++index1)
      {
        for (int index2 = 0; index2 < m + 1; ++index2)
          a[index1, index2] = numArray2[index1 + index2];
      }
      return this.SystemSolution(m + 1, a, b);
    }

    private double[] SystemSolution(int n, double[,] a, double[] b)
    {
      double[] numArray = new double[n];
      double[,] mass = new double[n, n];
      double num = MainForm.CalcDeterminant(n, a);
      for (int index1 = 0; index1 < n; ++index1)
      {
        for (int index2 = 0; index2 < n; ++index2)
        {
          for (int index3 = 0; index3 < n; ++index3)
            mass[index2, index3] = index3 == index1 ? b[index2] : a[index2, index3];
        }
        numArray[index1] = MainForm.CalcDeterminant(n, mass) / num;
      }
      return numArray;
    }

    private static int FindColForSwap(int c, int sizeMatrix, double[,] mass)
    {
      for (int colForSwap = c + 1; colForSwap < sizeMatrix; ++colForSwap)
      {
        if (Math.Abs(mass[c, colForSwap]) > 1E-07)
          return colForSwap;
      }
      return 0;
    }

    private static void SwapColumns(int k, int l, int n, double[,] mass)
    {
      for (int index = 0; index < n; ++index)
      {
        double num = mass[index, k];
        mass[index, k] = mass[index, l];
        mass[index, l] = num;
      }
    }

    public static double CalcDeterminant(int n, double[,] mass)
    {
      double num1 = 1.0;
      double[,] mass1 = new double[n, n];
      for (int index1 = 0; index1 < n; ++index1)
      {
        for (int index2 = 0; index2 < n; ++index2)
          mass1[index1, index2] = mass[index1, index2];
      }
      for (int index3 = 0; index3 < n; ++index3)
      {
        if (Math.Abs(mass1[index3, index3]) < 1E-07)
        {
          int colForSwap = MainForm.FindColForSwap(index3, n, mass1);
          if (colForSwap == 0)
            return 0.0;
          num1 = -num1;
          MainForm.SwapColumns(index3, colForSwap, n, mass1);
        }
        double num2 = mass1[index3, index3];
        for (int index4 = index3 + 1; index4 < n; ++index4)
        {
          double num3 = mass1[index4, index3];
          for (int index5 = index3; index5 < n; ++index5)
            mass1[index4, index5] = -mass1[index3, index5] * num3 / num2 + mass1[index4, index5];
        }
      }
      for (int index = 0; index < n; ++index)
        num1 *= mass1[index, index];
      return num1;
    }

    private void BtnCalc_Click(object sender, EventArgs e)
    {
      this.GetData();
      this.PoliApproximation();
    }

    private void PoliApproximation()
    {
      int m = (int) this.NudRank.Value;
      double[] approximationCoeff = this.GetPolinomialApproximationCoeff(m);
      string str = "P" + (object) m + "(x)=";
      for (int index = 0; index < m + 1; ++index)
        str = str + (approximationCoeff[index] > 0.0 ? (object) " +" : (object) "") + string.Format("{0:N2}", (object) approximationCoeff[index]) + "*x^" + (object) index + " ";
      for (int index = 0; index < this.N; ++index)
      {
        double num = this.Polinomial(approximationCoeff, this.Data[0, index]);
        this.DgvInitialData.Rows[2].Cells[index].Value = (object) string.Format("{0:N1}", (object) num);
      }
      this.RtbxApproxPolinomial.Text = str;
    }

    private double Polinomial(double[] a, double x)
    {
      int length = a.Length;
      double num = 0.0;
      for (int y = 0; y < length; ++y)
        num += a[y] * Math.Pow(x, (double) y);
      return num;
    }

    private void PowApproximation()
    {
      double[] approximationCoeff = this.GetPowApproximationCoeff();
      this.RtbxPow.Text = "F(x) = a * x^b = " + string.Format("{0:N2}", (object) approximationCoeff[0]) + " * x^" + string.Format("{0:N2}", (object) approximationCoeff[1]);
      for (int index = 0; index < this.N; ++index)
        this.DgvInitialData.Rows[3].Cells[index].Value = (object) string.Format("{0:N1}", (object) this.PowFunction(approximationCoeff, this.Data[0, index]));
    }

    private double[] GetPowApproximationCoeff()
    {
      double[] approximationCoeff = new double[2];
      double num1 = 0.0;
      double x = 0.0;
      double num2 = 0.0;
      double num3 = 0.0;
      for (int index = 0; index < this.N; ++index)
      {
        num1 += Math.Log(this.Data[1, index], Math.E);
        x += Math.Log(this.Data[0, index], Math.E);
        num2 += Math.Pow(Math.Log(this.Data[0, index], Math.E), 2.0);
        num3 += Math.Log(this.Data[1, index], Math.E) * Math.Log(this.Data[0, index], Math.E);
      }
      double num4 = (double) this.N * num2 - Math.Pow(x, 2.0);
      approximationCoeff[1] = ((double) this.N * num3 - num1 * x) / num4;
      double d = (num1 * num2 - num3 * x) / num4;
      approximationCoeff[0] = Math.Exp(d);
      return approximationCoeff;
    }

    private void BtnCalcPow_Click(object sender, EventArgs e)
    {
      this.GetData();
      this.PowApproximation();
    }

    private double PowFunction(double[] coef, double x) => coef[0] * Math.Pow(x, coef[1]);

    private void ExpApproximation()
    {
      double[] approximationCoeff = this.GetExpApproximationCoeff();
      this.RtbxExp.Text = "F(x) = a * e^(b*x) = " + string.Format("{0:N2}", (object) approximationCoeff[0]) + " * e^(" + string.Format("{0:N2}", (object) approximationCoeff[1]) + "*x)";
      for (int index = 0; index < this.N; ++index)
        this.DgvInitialData.Rows[4].Cells[index].Value = (object) string.Format("{0:N1}", (object) this.ExpFunction(approximationCoeff, this.Data[0, index]));
    }

    private double[] GetExpApproximationCoeff()
    {
      double[] approximationCoeff = new double[2];
      double num1 = 0.0;
      double x = 0.0;
      double num2 = 0.0;
      double num3 = 0.0;
      for (int index = 0; index < this.N; ++index)
      {
        num1 += Math.Log(this.Data[1, index], Math.E);
        x += this.Data[0, index];
        num2 += Math.Pow(this.Data[0, index], 2.0);
        num3 += Math.Log(this.Data[1, index], Math.E) * this.Data[0, index];
      }
      double num4 = (double) this.N * num2 - Math.Pow(x, 2.0);
      approximationCoeff[1] = ((double) this.N * num3 - num1 * x) / num4;
      double d = (num1 * num2 - num3 * x) / num4;
      approximationCoeff[0] = Math.Exp(d);
      return approximationCoeff;
    }

    private void BtnCalcExp_Click(object sender, EventArgs e)
    {
      this.GetData();
      this.ExpApproximation();
    }

    private double ExpFunction(double[] coef, double x) => coef[0] * Math.Exp(coef[1] * x);

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      this.DgvInitialData = new DataGridView();
      this.Column1 = new DataGridViewTextBoxColumn();
      this.label1 = new Label();
      this.label2 = new Label();
      this.NudRank = new NumericUpDown();
      this.BtnCalc = new Button();
      this.label3 = new Label();
      this.RtbxApproxPolinomial = new RichTextBox();
      this.label4 = new Label();
      this.BtnCalcPow = new Button();
      this.label5 = new Label();
      this.RtbxPow = new RichTextBox();
      this.RtbxExp = new RichTextBox();
      this.label6 = new Label();
      this.BtnCalcExp = new Button();
      ((ISupportInitialize) this.DgvInitialData).BeginInit();
      this.NudRank.BeginInit();
      this.SuspendLayout();
      this.DgvInitialData.AllowUserToAddRows = false;
      this.DgvInitialData.AllowUserToDeleteRows = false;
      this.DgvInitialData.AllowUserToOrderColumns = true;
      this.DgvInitialData.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.DgvInitialData.BackgroundColor = Color.White;
      gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gridViewCellStyle1.BackColor = SystemColors.Control;
      gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      gridViewCellStyle1.ForeColor = SystemColors.WindowText;
      gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
      this.DgvInitialData.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
      this.DgvInitialData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DgvInitialData.Columns.AddRange((DataGridViewColumn) this.Column1);
      this.DgvInitialData.Location = new Point(12, 32);
      this.DgvInitialData.Name = "DgvInitialData";
      this.DgvInitialData.RowHeadersWidth = 95;
      this.DgvInitialData.SelectionMode = DataGridViewSelectionMode.CellSelect;
      this.DgvInitialData.Size = new Size(739, 140);
      this.DgvInitialData.TabIndex = 0;
      gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.Column1.DefaultCellStyle = gridViewCellStyle2;
      this.Column1.HeaderText = "Column1";
      this.Column1.Name = "Column1";
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.label1.ForeColor = Color.DarkBlue;
      this.label1.Location = new Point(14, 12);
      this.label1.Name = "label1";
      this.label1.Size = new Size(58, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Данные:";
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.label2.ForeColor = Color.DarkBlue;
      this.label2.Location = new Point(14, 194);
      this.label2.Name = "label2";
      this.label2.Size = new Size(258, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Степень аппроксимирующего полинома : ";
      this.NudRank.Location = new Point(287, 190);
      this.NudRank.Maximum = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      this.NudRank.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.NudRank.Name = "NudRank";
      this.NudRank.Size = new Size(35, 20);
      this.NudRank.TabIndex = 3;
      this.NudRank.TextAlign = HorizontalAlignment.Right;
      this.NudRank.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.BtnCalc.Location = new Point(12, 226);
      this.BtnCalc.Name = "BtnCalc";
      this.BtnCalc.Size = new Size(310, 23);
      this.BtnCalc.TabIndex = 4;
      this.BtnCalc.Text = "Расчитать коэффициенты полинома";
      this.BtnCalc.UseVisualStyleBackColor = true;
      this.BtnCalc.Click += new EventHandler(this.BtnCalc_Click);
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.label3.ForeColor = Color.FromArgb(0, 64, 0);
      this.label3.Location = new Point(12, 267);
      this.label3.Name = "label3";
      this.label3.Size = new Size(189, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Аппроксимирующий полином :";
      this.RtbxApproxPolinomial.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.RtbxApproxPolinomial.BorderStyle = BorderStyle.None;
      this.RtbxApproxPolinomial.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.RtbxApproxPolinomial.Location = new Point(211, 267);
      this.RtbxApproxPolinomial.Name = "RtbxApproxPolinomial";
      this.RtbxApproxPolinomial.ReadOnly = true;
      this.RtbxApproxPolinomial.Size = new Size(540, 43);
      this.RtbxApproxPolinomial.TabIndex = 6;
      this.RtbxApproxPolinomial.Text = "";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(332, 194);
      this.label4.Name = "label4";
      this.label4.Size = new Size(147, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "(при значении 1 - линейная)";
      this.BtnCalcPow.Location = new Point(12, 317);
      this.BtnCalcPow.Name = "BtnCalcPow";
      this.BtnCalcPow.Size = new Size(310, 23);
      this.BtnCalcPow.TabIndex = 8;
      this.BtnCalcPow.Text = "Расчитать коэффициенты степенной функции";
      this.BtnCalcPow.UseVisualStyleBackColor = true;
      this.BtnCalcPow.Click += new EventHandler(this.BtnCalcPow_Click);
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.label5.ForeColor = Color.FromArgb(0, 64, 0);
      this.label5.Location = new Point(12, 356);
      this.label5.Name = "label5";
      this.label5.Size = new Size(128, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "Логарифмическая функция:";
      this.RtbxPow.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.RtbxPow.BorderStyle = BorderStyle.None;
      this.RtbxPow.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.RtbxPow.Location = new Point(211, 356);
      this.RtbxPow.Name = "RtbxPow";
      this.RtbxPow.ReadOnly = true;
      this.RtbxPow.Size = new Size(540, 24);
      this.RtbxPow.TabIndex = 10;
      this.RtbxPow.Text = "";
      this.RtbxExp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.RtbxExp.BorderStyle = BorderStyle.None;
      this.RtbxExp.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.RtbxExp.Location = new Point(211, 438);
      this.RtbxExp.Name = "RtbxExp";
      this.RtbxExp.ReadOnly = true;
      this.RtbxExp.Size = new Size(542, 24);
      this.RtbxExp.TabIndex = 13;
      this.RtbxExp.Text = "";
      this.label6.AutoSize = true;
      this.label6.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.label6.ForeColor = Color.FromArgb(0, 64, 0);
      this.label6.Location = new Point(12, 438);
      this.label6.Name = "label6";
      this.label6.Size = new Size(178, 13);
      this.label6.TabIndex = 12;
      this.label6.Text = "Экспоненциальная функция:";
      this.BtnCalcExp.Location = new Point(12, 399);
      this.BtnCalcExp.Name = "BtnCalcExp";
      this.BtnCalcExp.Size = new Size(310, 23);
      this.BtnCalcExp.TabIndex = 11;
      this.BtnCalcExp.Text = "Расчитать коэффициенты экспоненциальной функции";
      this.BtnCalcExp.UseVisualStyleBackColor = true;
      this.BtnCalcExp.Click += new EventHandler(this.BtnCalcExp_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(763, 477);
      this.Controls.Add((Control) this.RtbxExp);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.BtnCalcExp);
      this.Controls.Add((Control) this.RtbxPow);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.BtnCalcPow);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.RtbxApproxPolinomial);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.BtnCalc);
      this.Controls.Add((Control) this.NudRank);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.DgvInitialData);
      this.Name = nameof (MainForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Аппроксимация";
      ((ISupportInitialize) this.DgvInitialData).EndInit();
      this.NudRank.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
