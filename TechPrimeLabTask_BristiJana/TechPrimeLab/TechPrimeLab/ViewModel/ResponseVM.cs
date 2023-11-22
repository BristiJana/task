namespace TechPrimeLab.ViewModel
{
    public class ResponseVM
    {
        internal bool Success;
        internal object? ErrorMessage;

        public bool flag { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
        public dynamic response { get; set; }
        public ResponseVM(bool Flag, int StatusCode, string Message, dynamic Data)
        {
            flag = Flag;
            status_code = StatusCode;
            message = Message;
            response = Data;
        }
    }
}
