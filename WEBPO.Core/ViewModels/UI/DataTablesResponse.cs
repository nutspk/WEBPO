using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Core.ViewModels
{
    public class DataTablesResponse
    {
        public int draw { get; }
        public IEnumerable data { get; }
        public int recordsTotal { get; }
        public int recordsFiltered { get; }

        public DataTablesResponse(int draw, IEnumerable data, int recordsTotal, int recordsFiltered)
        {
            this.draw = draw;
            this.data = data;
            this.recordsTotal = recordsTotal;
            this.recordsFiltered = recordsFiltered;
        }

        public string ToJson() {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, settings);
        }
    }

}
