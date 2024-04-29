namespace BusinessLogic.Exceptions
{
    public sealed class RegionNotFoundException : NotFoundException
    {
        public RegionNotFoundException(int id)
            : base($"The region with the id: {id} was not found") 
        { 
        }
    }
}