using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace bulkyApp.Models
{
    public class ApiResponseModel
    {

        public string msg { get; set; }
        public bool success { get; set; }

        public List<dynamic>? listData { get; set; }
        public dynamic? singleData { get; set; }
        public string? stringData { get; set; }

		public static implicit operator ApiResponseModel?(ApiExplorerModel? v)
		{
			throw new NotImplementedException();
		}
	}
}
