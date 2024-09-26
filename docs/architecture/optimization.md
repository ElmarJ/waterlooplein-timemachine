# Thoughts on Optimization

Below a few thoughts, ideas and links to relevant resources on (further) optimizing the game:

## Specific considerations for this game

### Buildings as Static GameObjects

Because of the time-machine functionality, currently all buildings and even "Canal-Islands" (making up the pavement and streets we're walking on) are set up as non-static GameObjects. The reason is that some buildings and structures _could_ disappaer when time-traveling. Still, these objects don't move and are 99% of the time static. Even when they disappaer, they are often simply replaced by buildings with very similar dimensions.

#### What would it solve?

Given that these generated meshes make up almost everything that is rendered, this has serious performance repercussions as baking is almost impossible. Some problems we see:

- We cannot bake data for *Occlusion Culling*, even though it seems to make much sense given that the vast majority of objects are occluded most of the time by other objects. One of the results is that water is always rendered, even when there is no water in sight. Water rendering greatly affects GPU-performance. The same happens with clouds (although the impact is smaller). Note that [Occlusion Culling can also increase CPU-cost](https://docs.unity3d.com/Manual/OcclusionCulling.html), so does not necessarily lead to performance improvements.

- We have no baked Shadow data and GI-data. Computing realtime shadows seems to claim a lot of performance in the app (needs more research). 

#### How to solve?

There is no simple solution. In the past, I have experimented with marking _some_ buildings as static, but this (obviously) led to weird artifacts when the buildings did disappear (but were still used in occlusion data).

##### Multiple time Scenes

A complete solution would to split up the TimeMachine-effect over several Scenes, each capturing a different time frame. If these time frames are well chosen (to maximize the number of objects present during the full period), we could make most buildings "static" in these Scenes.

Some disadvantages to this approach:

- Will have a large performance impact on the time-machine time-scrolling itself. I do like the current "fluent" year-slider-effect, we will quite likely lose that.

- Is a lot of work, and introduces many complexities related to having to keep all the different Scenes "in synch" with each other in development. Problably best solution is to keep developing in a single Scene, and then automatically split the Scene right before Build, but this requires a rather sizable coding effort.

##### Other occlusion-solutions

There might be other approaches to make sure that occluded objects are not visible. Some things I could do:

- Automatically mark all objects that are always present during the full covered period (currently 1800-2000) as static. This would not solve everything (the whole point of this simulation is that most buildings in the area of interest have been demolished at some point in time), but it might still help (particularly for far-away scenery).

- Work with layers and a Culling Mask to only render specific buildings (that are visible from great distance) from larger distances and setup smaller "clipping" distances for most objects.

## Resources

### General Optimization

- A thorough discussion of possible starting points for performance optimization (and common problems) can be found in [the "Fixing Performance Problems" course on Unity Learn](https://learn.unity.com/tutorial/fixing-performance-problems-2019-3#60459198edbc2a3ba0d199f3).

- More starting points for optimization [are discussed in the Unity Documentation](https://docs.unity3d.com/6000.0/Documentation/Manual/UnderstandingPerformance.html).

### Profiling

- [A Unity Learn course on Diagnosing Performance Problems](https://learn.unity.com/tutorial/diagnosing-performance-problems-2019-3) with the profiler.

### Frame Debugger

- [Unity Manual on using the Frame Debugger](https://docs.unity3d.com/6000.0/Documentation/Manual/profiling-landing.html)

- [Unity Video Tutorial about Frame Debugger](https://www.youtube.com/watch?v=4N8GxCeolzM)

### Other Tools

- RenderDoc
- NVidia Nsight
- Microsoft PIX