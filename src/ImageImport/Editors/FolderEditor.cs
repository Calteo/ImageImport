using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace ImageImport.Editors
{
    internal class FolderEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {                
                var browser = new FolderBrowserDialog
                {
                    SelectedPath = (string)value
                };

                var form = new Form
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                form.Load += (s, e) =>
                {
                    if (browser.ShowDialog(form) == DialogResult.OK)
                    {
                        form.DialogResult = DialogResult.OK;
                    }
                    form.Close();
                };

                if (svc.ShowDialog(form) == DialogResult.OK)
                {
                    return browser.SelectedPath;
                }                    
            }
            return value;
        }
    }
}
