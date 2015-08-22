namespace DynamicFieldExplorer

open System
open UIKit
open CoreGraphics

type WanderingViewController(backgroundColor, title, frame : CGRect, fieldBehavior : UIFieldBehavior) as this = 
    inherit UIViewController()

    let wanderers = 
            seq { 20.0 .. 60.0 .. (Math.Floor(float frame.Height)) }
            |> Seq.map (fun y -> new CGRect(20.0, y, 20.0, 20.0))
            |> Seq.map (fun frame -> 
                let v = new UIView(frame)
                v.BackgroundColor <- UIColor.Red
                v
                ) |> Array.ofSeq

    do
        this.View.BackgroundColor <- backgroundColor
        this.Title <- title

        this.View.AddSubviews(wanderers)

    override this.ViewDidLoad() = 
        base.ViewDidLoad()


    override this.ViewWillAppear(v) = 
        base.ViewWillAppear(v)
        let animator = new UIDynamicAnimator(this.View)

        fieldBehavior.Position <- new CGPoint(this.View.Frame.Left + this.View.Frame.Width / nfloat 2.0, this.View.Frame.Top + this.View.Frame.Height / nfloat 2.0)
      //  fieldBehavior.Region <- new UIRegion(new CGSize(this.View.Frame.Width, this.View.Frame.Height / nfloat 3.0))
        fieldBehavior.MinimumRadius <- nfloat 5.0

        wanderers 
        |> Seq.map (fun w -> new UIDynamicItemBehavior([| w :> IUIDynamicItem|])) 
        |> Seq.iter (fun itemBehavior ->
            itemBehavior.Density <- nfloat 0.01
            itemBehavior.Resistance <- nfloat 0.0
            itemBehavior.Friction <- nfloat 0.0
            itemBehavior.AllowsRotation <- true
            itemBehavior.Charge <- nfloat -1.0
            animator.AddBehavior(itemBehavior)
            )
        
        wanderers |> Seq.iter fieldBehavior.AddItem

        animator.AddBehavior(fieldBehavior)
        animator.PerformSelector(new ObjCRuntime.Selector("setDebugEnabled:"), Foundation.NSObject.FromObject(true)) |> ignore

        //Begin the wandering
        wanderers
        |> Seq.iter (fun w -> 
            let b = new UIDynamicItemBehavior([| w :> IUIDynamicItem |])
            b.AddLinearVelocityForItem(new CGPoint(nfloat 10.0, nfloat 10.0), w)
            b.AddAngularVelocityForItem(nfloat 2.0, w)
            animator.AddBehavior(b)
            )
        
    override this.TouchesBegan(sender, touches) = ignore()
