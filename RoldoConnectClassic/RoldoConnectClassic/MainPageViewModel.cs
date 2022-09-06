using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RoldoConnectClassic
{
    public class MainPageViewModel:ModelBase
    {
        public Func<IList<char>, bool> PinValidator { get;}

       // public ICommand SwitchPinLengthCommand { get; }

        public ICommand ErrorCommand { get; }

        public ICommand SuccessCommand { get; }

        private int _pinLength;
        public int PinLength
        {
            get => _pinLength;
            private set
            {
                _pinLength = value;
                RaisePropertyChanged(nameof(PinLength));
            }
        }

        private string _pin = "0000"; //password is 1234

        public MainPageViewModel()
        {
            PinValidator = (arg) =>
            {
                if (Application.Current.Properties.ContainsKey("password"))
                {
                    _pin = Application.Current.Properties["password"] as string;
                }
                Application.Current.Properties["oldpassword"] = _pin;

                if (arg.Count == _pin.Length)
                {
                    for (int i = 0; i < arg.Count; ++i)
                    {
                        if (arg[i] != _pin[i])
                        {
                            if (Application.Current.Properties.ContainsKey("password"))
                            {
                                return false;
                            }
                            else
                            {
                                Application.Current.Properties["password"] = string.Join("", arg);
                                return true;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    Debug.WriteLine(" Check the private pin,  variable name is _pin");
                    return false;
                }
                  
            };
            PinLength = 4;

            //SwitchPinLengthCommand = new Command(() =>
            //{
            //    PinLength = PinLength == 4 ? 6 : 4;
            //});

            ErrorCommand = new Command(() =>
            {
                Debug.WriteLine("Entered PIN is wrong");
            });

            SuccessCommand = new Command(() =>
            {
                Debug.WriteLine("Entered PIN is correct");
            });
        }

    }
}


