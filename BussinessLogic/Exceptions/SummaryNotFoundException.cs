namespace BusinessLogic.Exceptions
{
    public sealed class SummaryNotFoundException : NotFoundException
    {
        public SummaryNotFoundException(int id)
            : base($"The summary with the id: {id} was not found") 
        { 
        }
    }
}