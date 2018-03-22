
namespace MpesaLib
{
    public class B2C
    {
        public string InitiatorName { get; set; } = "testapi";
        public string SecurityCredential { get; set; } = "FE1MVFVDYtgrsJMfWYd8w4pxeup1fhR/qjgcacwA1JiWgn1eSXCc28+N00fm213AWr7yg4ltL+jFJG9szVcuQ2wtPywrH80a0lU9WqycBMWL7C6G6oRrD/mAeeFxfDnLY3yx5D9Anp+GlC3LH+srThvPbNoU4ZJiMwKq4IDedjVPza8rf1rjW4in4zxbbX2Z3++xUWVqix6rCwLCNNHgV7OzrLljCRQdI+hzCfjsppHc8gy5hxHjW3QoaBCBxGHwlhs/jJxh/dv37JxK5y85rfbqd/Vv51RIfvfsxurY/bH7OoKQ06XFCHl6cC27rPMaRsDp9GM7WJkXLYt4SE6dtg==";
        public string CommandID { get; set; } = "BusinessPayment";
        public string Amount { get; set; } = "100";
        public string PartyA { get; set; } = "600525";
        public string PartyB { get; set; } = "254708374149";
        public string Remarks { get; set; } = "test";
        public string QueueTimeOutURL { get; set; } = "https://hookbin.com/bin/Z8aaN0Ob";
        public string ResultURL { get; set; } = "https://hookbin.com/bin/Z8aaN0Ob";
        public string Occasion { get; set; } = "test";
    }
}
