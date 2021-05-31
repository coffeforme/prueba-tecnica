using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Text;

namespace core.util.json
{
    public class jsonresponse<T> : responsebase<T>
    {
        public jsonresponse() { }

        public jsonresponse(string response, jsonstatus status, bool done, Exception error = null)
        {
            this.response = response;
            this.status = status;
            this.done = done;
            this.error = error;
        }

    }

    public class jsonresponse : responsebase<object>
    {
        public jsonresponse() { }
        public jsonresponse(responsebase<object> origin) : base(origin) { }
        public jsonresponse(string response, jsonstatus status, bool done, Exception error = null)
        {
            this.response = response;
            this.status = status;
            this.done = done;
            this.error = error;
        }

    }

    public class responsebase<T>
    {
        [JsonConverter(typeof(StringEnumConverter), true)]
        public jsonstatus status { get; set; }
        public bool done { get; set; }
        public Exception error { get; set; }
        public string response { get; set; }
        public T data { get; set; }

        public responsebase() { }
        public responsebase(responsebase<T> origin)
        {
            this.status = origin.status;
            this.data = origin.data;
            this.response = origin.response;
            this.done = origin.done;
            this.error = origin.error;
        }

        public responsebase<object> copyfromtyped()
        {
            return new responsebase<object>()
            {
                status = this.status,
                data = this.data,
                response = this.response,
                done = this.done,
                error = this.error
            };
        }

        public responsebase<S> copywithoutdata<S>()
        {
            return new responsebase<S>()
            {
                status = this.status,
                response = this.response,
                done = this.done,
                error = this.error
            };
        }

        public string logresponse()
        {
            StringBuilder bl = new StringBuilder();
            bl.Append($"Transaction status: {Enum.GetName(typeof(jsonstatus), status)}");
            bl.Append($"Transaction message: {response}");
            if (data != null)
                bl.AppendLine(JsonConvert.SerializeObject(data).ToString());
            if (!done)
            {
                bl.Append($"Transaction error: {error.Message}");
                bl.AppendLine(JsonConvert.SerializeObject(error).ToString());
            }


            return bl.ToString();
        }
    }
}
