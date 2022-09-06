using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RoldoConnectClassic
{
    public static class Transporter
    {
        public static IBluetoothLE ble;
        public static IAdapter adapter;
        public static ObservableCollection<IDevice> deviceList;
        public static IDevice device;
    }

}
