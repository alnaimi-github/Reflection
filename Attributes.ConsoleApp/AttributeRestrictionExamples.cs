using System.ComponentModel;
using System.Runtime.CompilerServices;

public sealed class AttributeRestrictionExamples
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CustomClassAttribute : Attribute
    {
        public CustomClassAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CustomMethodAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class CustomParameterAttribute : Attribute
    {
    }

    [CustomClass("Super Cool Class Name")]
    public class OurDecoratedClass
    {
        //[CustomClass] // this won't work!
        [CustomMethod]
        public void OurMethod(
            [CustomParameter] string theInput)
        {

        }
    }

    public sealed class ObserableType : INotifyPropertyChanged
    {
        private string? _name;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? Name
        {
            get => _name;
            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}