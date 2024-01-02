
using Android.Content;
using Appmeds;
using Android.Content.Res;
using Android.Graphics;
using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CustomRenderer.Android;
using System.Reflection;
using Android.Graphics.Drawables;

// [assembly: ExportRenderer(typeof(CustomEntry) , (typeof(CustomEntryRenderer))]

namespace CustomRenderer.Android
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context): base(context)
        { 
        
        
        }

        protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged (e);

            if (Control!=null)
            {
                GradientDrawable gradientDrawable = new GradientDrawable();
                gradientDrawable.SetColor(global::Android.Graphics.Color.Transparent);
                Control.SetBackground(gradientDrawable);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                Control.SetHintTextColor(global::Android.Graphics.Color.Aqua);


            }
        }
    }
}