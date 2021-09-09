
namespace Text_Adventure
{
    class test
    {
        static void Main(string[] args)
        {
            Object test = new Object("test", null);
            ObjectFunctions obj_funcs = new ObjectFunctions();
            test.setInteractMethod(obj_funcs.Called);
            
            test.callInteractMethod();
        }
    }
}