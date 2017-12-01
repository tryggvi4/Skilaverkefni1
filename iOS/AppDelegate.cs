using System.Drawing;
using Foundation;
using UIKit;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using MovieSearchB.iOS.Controllers;

namespace MovieSearchB.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        MovieDbConnection database;

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method
            database = new MovieDbConnection();

            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

            this.Window = new UIWindow(UIScreen.MainScreen.Bounds);
            var movieController = new MovieSearchViewController(movieApi);
            var movieNavigationController = new UINavigationController(movieController);

            var topRatedController = new TopRatedListController(movieApi);
            var topRatedNavigationController = new UINavigationController(topRatedController);

            var tabBarController = new TabBarController()
            {
                ViewControllers = new UIViewController[] { movieNavigationController, topRatedNavigationController} //TODO: Bæta við Collection
            };

            this.Window.RootViewController = tabBarController;
            this.Window.MakeKeyAndVisible();

            return true;
        }

        private static UICollectionViewFlowLayout CreateFlowLayout()
        {
            return new UICollectionViewFlowLayout()
            {
                MinimumInteritemSpacing = 5,
                MinimumLineSpacing = 5,
                ItemSize = new SizeF(80, 80)
            };
        }


        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}

