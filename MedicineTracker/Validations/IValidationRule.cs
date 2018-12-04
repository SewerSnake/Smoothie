namespace MedicineTracker.Validation
{
    public interface IValidationRule<T>
    {
        bool Check(T value);
    }
}