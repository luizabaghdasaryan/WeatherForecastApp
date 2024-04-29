namespace BusinessLogic.Exceptions
{
    public sealed class ForecastNotFoundException : NotFoundException
    {
        public ForecastNotFoundException(int id)
            : base($"The forecast with the id: {id} was not found") 
        { 
        }

        public ForecastNotFoundException(DateTime date, int regionId)
            : base($"The forecast with the date: {date:dd-MM-yyyy} and regionId: {regionId} was not found")
        {
        }
    }
}