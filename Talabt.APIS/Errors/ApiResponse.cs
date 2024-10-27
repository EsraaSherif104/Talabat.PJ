
namespace Talabt.APIS.Errors
{
    public class ApiResponse
    {
        public int Statuscode {  get; set; }   
        public string? Message { get; set; }
        public ApiResponse(int statuscode,string? message=null)
        {
            Statuscode=statuscode;
            Message=message??GetDefaultMessageForStatuesCode(statuscode);
        }

        private string? GetDefaultMessageForStatuesCode(int? statuscode)
        {
            //500=> internal server error
            //400=>bad request
            //401=>unthorized
            //404=>not found 

            return Statuscode switch
            {
                400 => "Bad Request",
                401=>"yoy are not Authorized",
                404=>"Resource not found",
                500=>"Internal Server Error",
               _ =>null

            };
        }
    }
}
