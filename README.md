# DoodlebugGH
A plug-in for GH to connect Grasshopper to Illustrator. 
Fork of the original work from Andrew Heumann at https://bitbucket.org/andheum/doodlebuggh/src/master/

# Setup the repo

Clone to your hard drive and launch the .sln in visual studio. You may need to re-point the references for RhinoCommon and Grasshopper in the plug-in project, and you may also need to re-register the COM interface reference in the IllustratorInterface project - delete Illustrator from the references, right-click, "Add reference," go to COM, and locate the Adobe Illustrator Type Library and add it. I have been leaving this reference set to "Embed Interop Types" in the Reference Properties. 

# Install the plug-in
Copy these files in your Grasshopper library folder:
- illest.dll
- IllustratorInterop.dll
- Doodlebug.gha

if you download them from the Release page of this repo, you might need to unblock them:

(right click -> properties -> unblock) 

and restart Rhino/Grasshopper

# General info
main branch is used for development, release branch for releases.
