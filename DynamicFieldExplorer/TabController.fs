namespace DynamicFieldExplorer

open System
open UIKit
open CoreGraphics

type TabController() as this = 
    inherit UITabBarController()

    let bounds = UIScreen.MainScreen.Bounds 
    let midpoint = new CGPoint(bounds.Left + bounds.Width / nfloat 2.0, bounds.Top + bounds.Height / nfloat 2.0)

    let tabs : UIViewController[] = [|
        new WanderingViewController(UIColor.Green, "Spring", bounds, UIFieldBehavior.CreateSpringField());
        new WanderingViewController(UIColor.Blue, "Drag", bounds, UIFieldBehavior.CreateDragField());
        new WanderingViewController(UIColor.Cyan, "Electricity", bounds, UIFieldBehavior.CreateElectricField());
        new WanderingViewController(UIColor.Orange, "Linear gravity", bounds, UIFieldBehavior.CreateLinearGravityField(new CGVector(nfloat 0.0, nfloat 5.0)));
        new WanderingViewController(UIColor.Yellow, "Radial gravity", bounds, UIFieldBehavior.CreateRadialGravityField(midpoint));
        new WanderingViewController(UIColor.Green, "Magnetic", bounds, UIFieldBehavior.CreateMagneticField());
        new WanderingViewController(UIColor.Blue, "Noise", bounds, UIFieldBehavior.CreateNoiseField(nfloat 1.0, nfloat 3.0));
        new WanderingViewController(UIColor.Cyan, "Turbulence", bounds, UIFieldBehavior.CreateTurbulenceField(nfloat 1.0, nfloat 3.0));
        new WanderingViewController(UIColor.Orange, "Velocity", bounds, UIFieldBehavior.CreateVelocityField(new CGVector(nfloat 0.0, nfloat 5.0)));
        new WanderingViewController(UIColor.Yellow, "Vortex", bounds, UIFieldBehavior.CreateVortexField());
        
    |]

    do
        this.ViewControllers <- tabs

    member val Tabs = tabs with get


