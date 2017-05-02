# Game Engine Assignment Document
Yifan Xu
D15122839

## Introduction

In my assignment of game engines module, I planned to create a school of Android robots with walk animator. Follow path behavior and wander behavior are implemented to make robots can interact with the user or move automatically. The scenario of this assignment is set in a small room, which includes obstacles and walls. An implementation of avoiding collision behavior might help robot avoiding the walls and obstacle in the room. Lead behavior was supposed to let several robots follow a leader.

## Video

[![Unity Android Robot Creator](http://i3.ytimg.com/vi/8ZUMnKosAeM/maxresdefault.jpg)](http://www.youtube.com/embed/8ZUMnKosAeM?)

## Behaviors

    * Wander Behaviour

Wander Behaviour makes creator move in an environment randomly. In Forms, it is used to some creator, such as fish and bird, to enable them to move in 3D space. in terms of a standing creator, I slight change the wandering behavior to make it generate a force in the horizontal direction. So, It generates a force in the vertical direction.

[More Wander Behavior](https://gamedevelopment.tutsplus.com/tutorials/understanding-steering-behaviors-wander--gamedev-1624)

    * Follow Path Behavior

Based on regular follow path behavior, I modified the behavior allow the user to add a new waypoint into the existing path via click. the robot can follow the path that is provided by the user.  When the robot complete the path, the waypoint will be deleted from the path.

[More Follow Path Behavior](https://gamedevelopment.tutsplus.com/tutorials/understanding-steering-behaviors-path-following--gamedev-8769)

    * Avoid Collision Behavior

I created 3 feelers for each of robot. sphere cast will happen with every feeler. When the sphere collides with some object, it will return the information about this collision and generate a normal force.

[More Avoid Collision Behavior](https://gamedevelopment.tutsplus.com/tutorials/understanding-steering-behaviors-collision-avoidance--gamedev-7777)

    * Leader Behaviour

Leader Behavior enables robots as a school and follows a leader creator. The behavior will update a point behind the leader. other followers will seek the point. I write two functions to solve crowd block problem

[More Leader Behavior](https://gamedevelopment.tutsplus.com/tutorials/understanding-steering-behaviors-leader-following--gamedev-10810)

## State Machine

There are two states in this assignment. one is wandering state with which the robot will execute wander behavior. another is following behavior which makes the robot follow the path that the user provides. the state switch is triggered by some event like a click.

## Other

I also pay some attention to the implementation of general unity programming. I implemented a robot walking behavior with SLERP and trigonometric functions. types of camera controller are implemented in this system with the support of keyboard, mouse, and DualShock.
