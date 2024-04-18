namespace PrEParateApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterForRoute<MainPage>();


        }

        protected void RegisterForRoute<T>()
        {
            Routing.RegisterRoute(typeof(T).Name, typeof(T));
        }
    }
}
