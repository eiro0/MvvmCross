using System;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Droid.Interfaces.Views;
using Cirrious.MvvmCross.Binding.Interfaces;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Platform.Diagnostics;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using CrossUI.Core.Builder;

namespace Cirrious.MvvmCross.AutoView.Droid.Builders
{
    public class MvxBindingPropertySetter : IPropertySetter
                                            , IMvxServiceConsumer
    {
        private IMvxBindingActivity _bindingActivity;
        private object _source;

        public MvxBindingPropertySetter(IMvxBindingActivity bindingActivity, object source)
        {
            _bindingActivity = bindingActivity;
            _source = source;
        }

        public void Set(object element, string targetPropertyName, string configuration)
        {
            try
            {
                var binding = this.GetService<IMvxBinder>().BindSingle(_source, element, targetPropertyName, configuration);
                _bindingActivity.RegisterBinding(binding);
            }
            catch (Exception exception)
            {
                MvxBindingTrace.Trace(MvxTraceLevel.Error, "Exception thrown during the view binding {0}", exception.ToLongString());
                throw;
            }
        }
    }
}