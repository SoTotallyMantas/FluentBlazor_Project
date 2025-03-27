namespace FluentBlazor_Project.HelperFunctions
{
    public static class DifferenceCheckHelper
    {
        public static bool UpdateIfDifferent<T>(T current, T updated)
        {
            if(!EqualityComparer<T>.Default.Equals(current, updated)) {
               
              
                return true;
            }
            return false;
        }
    }
}
