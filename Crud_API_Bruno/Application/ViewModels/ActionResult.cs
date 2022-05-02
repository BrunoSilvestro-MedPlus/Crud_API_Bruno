namespace Crud_API_Bruno.Application.ViewModels
{
    public class ActionResult<TBodyResult> where TBodyResult : class
    {
        public TBodyResult Result { get; set; }
        public string ReasonOfFail { get; set; }
        public bool Completed { get; set; }

        public ActionResult(string message, bool completed = false)
        {
            this.ReasonOfFail = message;
            this.Completed = completed;
        }

        public ActionResult(TBodyResult body)
        {
            Result = body;
            Completed = true;
        }
        public ActionResult()
        {
            Completed = true;
        }
    }

    public class ActionResult
    {
        public string Message { get; set; }
        public bool Completed { get; set; }

        public ActionResult(string message, bool completed = false)
        {
            this.Message = message;
            this.Completed = completed;
        }

        public ActionResult()
        {
            Completed = true;
        }
    }
}