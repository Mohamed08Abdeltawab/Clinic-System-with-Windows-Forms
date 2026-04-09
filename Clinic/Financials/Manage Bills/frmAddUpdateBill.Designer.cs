namespace Clinic.Financials.Manage_Bills
{
    partial class frmAddUpdateBill
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlVisitInfoWithFilter1 = new Clinic.Medical_Services.Visit.ctrlVisitInfoWithFilter();
            this.SuspendLayout();
            // 
            // ctrlVisitInfoWithFilter1
            // 
            this.ctrlVisitInfoWithFilter1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctrlVisitInfoWithFilter1.FilterEnabled = true;
            this.ctrlVisitInfoWithFilter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlVisitInfoWithFilter1.Location = new System.Drawing.Point(202, 43);
            this.ctrlVisitInfoWithFilter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlVisitInfoWithFilter1.Name = "ctrlVisitInfoWithFilter1";
            this.ctrlVisitInfoWithFilter1.Size = new System.Drawing.Size(813, 602);
            this.ctrlVisitInfoWithFilter1.TabIndex = 0;
            // 
            // frmAddUpdateBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.ctrlVisitInfoWithFilter1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmAddUpdateBill";
            this.Text = "frmAddUpdateBill";
            this.Load += new System.EventHandler(this.frmAddUpdateBill_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Medical_Services.Visit.ctrlVisitInfoWithFilter ctrlVisitInfoWithFilter1;
    }
}