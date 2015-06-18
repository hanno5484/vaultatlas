using System;
using System.Windows.Forms;

namespace VaultAtlas.DataModel.ModelUI
{
	/// <summary>
	/// Summary description for ProgressWindow.
	/// </summary>
	public class ProgressWindow : System.Windows.Forms.Form, IProgressCallback
	{
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.ProgressBar progressBar;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private String titleRoot = "";
		private readonly System.Threading.ManualResetEvent _initEvent = new System.Threading.ManualResetEvent(false);
		private readonly System.Threading.ManualResetEvent _abortEvent = new System.Threading.ManualResetEvent(false);
		private bool _requiresClose = true;

		public ProgressWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

        public bool ProgressVisible
        {
            get
            {
                return this.progressBar.Visible;
            }
            set
            {
                this.progressBar.Visible = value;
            }
        }

		#region Implementation of IProgressCallback
		/// <summary>
		/// Call this method from the worker thread to initialize
		/// the progress meter.
		/// </summary>
		/// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
		/// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
		public void Begin( int minimum, int maximum )
		{
			_initEvent.WaitOne();
		    Invoke(new Action(() =>
		    {
		        cancelButton.Enabled = true;
		        ControlBox = true;
		        progressBar.Minimum = minimum;
		        progressBar.Maximum = maximum;
		        progressBar.Value = minimum;
		        titleRoot = Text;
		    }));
		}

		/// <summary>
		/// Call this method from the worker thread to initialize
		/// the progress callback, without setting the range
		/// </summary>
		public void Begin()
		{
			_initEvent.WaitOne();
		    Invoke(new Action(() =>
		    {
		        cancelButton.Enabled = true;
		        ControlBox = true;
		    }));
		}

		/// <summary>
		/// Call this method from the worker thread to reset the range in the progress callback
		/// </summary>
		/// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
		/// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
		/// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
		public void SetRange( int minimum, int maximum )
		{
			_initEvent.WaitOne();
		    Invoke(new Action(() =>
		    {
		        progressBar.Minimum = minimum;
		        progressBar.Maximum = maximum;
		        progressBar.Value = minimum;
		        titleRoot = Text;
		    }));
		}

		/// <summary>
		/// Call this method from the worker thread to update the progress text.
		/// </summary>
		/// <param name="text">The progress text to display</param>
		public void SetText( String text )
		{
		    Invoke(new Action(() =>
		    {
		        label.Text = text;
		    }));
		}

		/// <summary>
		/// Call this method from the worker thread to increase the progress counter by a specified value.
		/// </summary>
		/// <param name="val">The amount by which to increment the progress indicator</param>
		public void Increment( int val )
		{
		    Invoke(new Action(() =>
		    {
		        progressBar.Increment(val);
		        UpdateStatusText();
		    }));
		}

		/// <summary>
		/// Call this method from the worker thread to step the progress meter to a particular value.
		/// </summary>
		/// <param name="val"></param>
		public void StepTo( int val )
		{
		    Invoke(new Action(() =>
		    {
		        progressBar.Value = val;
		        UpdateStatusText();
		    }));
		}

		
		/// <summary>
		/// If this property is true, then you should abort work
		/// </summary>
		public bool IsAborting
		{
			get
			{
				return _abortEvent.WaitOne( 0, false );
			}
		}

		/// <summary>
		/// Call this method from the worker thread to finalize the progress meter
		/// </summary>
		public void End()
		{
            try
            {
                if (_requiresClose)
                {
                    Invoke(new MethodInvoker(DoEnd));
                }
            }
            catch
            {
            }
		}
		#endregion

	    private void DoEnd()
		{
			this.Close();
		}

		#region Overrides
		/// <summary>
		/// Handles the form load, and sets an event to ensure that
		/// intialization is synchronized with the appearance of the form.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(System.EventArgs e)
		{
			base.OnLoad( e );
			ControlBox = false;
			_initEvent.Set();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Handler for 'Close' clicking
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			_requiresClose = false;
			AbortWork();
			base.OnClosing( e );
		}
		#endregion
		
		#region Implementation Utilities
		/// <summary>
		/// Utility function that formats and updates the title bar text
		/// </summary>
		private void UpdateStatusText()
		{
			Text = titleRoot + String.Format( " - {0}% complete", (progressBar.Value * 100 ) / (progressBar.Maximum - progressBar.Minimum) );
		}
		
		/// <summary>
		/// Utility function to terminate the thread
		/// </summary>
		private void AbortWork()
		{
			_abortEvent.Set();
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(8, 80);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(192, 23);
            this.progressBar.TabIndex = 1;
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Location = new System.Drawing.Point(8, 8);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(272, 64);
            this.label.TabIndex = 0;
            this.label.Text = "Starting operation...";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(208, 80);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ProgressWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(290, 114);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ProgressWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Operation in progress ...";
            this.ResumeLayout(false);

		}
		#endregion

        private void cancelButton_Click(object sender, EventArgs e)
        {
            AbortWork();
        }

	}
}
