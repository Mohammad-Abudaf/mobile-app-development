using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using Modbus;
public class AlarmCode : MonoBehaviour {
    private readonly ModbusServer _server = new ModbusServer();
    // Start is called before the first frame update
    private void Start() {
        localAlarmActivation.isOn = false;
        _server.port = 50000; 
        _server.StartModbusServer();
    }
    // Update is called once per frame
    private void Update() {
        localIpOutput.text = $"IP: {GetLocalIPAddress()}";
        localPortOutput.text = $"Port: {_server.port}";
        ActivateAlarm(localAlarmActivation.isOn);
        localAlarmActivation.GetComponentInChildren<Text>().text = AlarmActivation();
        SetModbusClock(_alarmSecond, _alarmMin, _alarmHour);
        remoteAlarmTime.text = ModbusClock();
        remoteAlarmCondition.text = ModbusActivationCondition();
    }
    
    public AudioSource speaker;
    public AudioClip[] setsOfTones;
    public Toggle localAlarmActivation;
    public InputField alarmTimeInput;
    public Text localIpOutput;
    public Text localPortOutput;
    public Text remoteAlarmTime;
    public Text remoteAlarmCondition;

    private ushort _alarmMin, _alarmSecond, _alarmHour;

    private void SetModbusClock(ushort seconds, ushort min, ushort hour) {
            _server.HoldingRegister[1313] = seconds; 
            _server.HoldingRegister[1314] = min; 
            _server.HoldingRegister[1315] = hour; 
    }
    private string ModbusClock() =>
        $"{_server.HoldingRegister[1315]:00}:{_server.HoldingRegister[1314]:00}:{_server.HoldingRegister[1313]:00}";

    private void ActivateAlarm(bool activation) {
        if (_alarmHour == DateTime.Now.Hour && _alarmMin == DateTime.Now.Minute && _alarmSecond == DateTime.Now.Second && activation) {
            speaker.Play();
        }
    }
    private string ModbusActivationCondition() => _server.Coil[1072]? "connected" : "Disconnected";
    private string AlarmActivation() => localAlarmActivation.isOn ? "Activated" : "Deactivated"; 
    public void SelectedTone(int index) => speaker.clip = setsOfTones[index];
    public void SoundLevel(float level) => speaker.volume = (int)level;
    public void SetAlarm(string userInput) {
        var clockArr = userInput.Split(':');
        try {
            _alarmHour = ushort.Parse(clockArr[0]);
            _alarmMin = ushort.Parse(clockArr[1]);
            _alarmSecond = ushort.Parse(clockArr[2]);

            if ((_alarmHour < 24) && (_alarmMin < 60) && (_alarmSecond < 60))
                // ReSharper disable once HeapView.BoxingAllocation
                alarmTimeInput.text = $"{_alarmHour:00}:{_alarmMin:00}:{_alarmSecond:00}";
            else
                alarmTimeInput.text = "Out of Range";
        } catch {
            alarmTimeInput.text = "00:00:00";
            foreach (var item in clockArr)
                print(item);
        }
    }
    private static string GetLocalIPAddress() {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList) {
            if (ip.AddressFamily == AddressFamily.InterNetwork) {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}