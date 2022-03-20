![Referencing](https://user-images.githubusercontent.com/33253710/159187284-dd2bef31-c994-4f48-85a1-da40c77f961d.jpg)

<p align=center>Classes to help with referencing between scripts.</p>

# Usage
The class is static so all you need to do is to use the classes method <code>GetComponentFromScene()</code> or <code>GetComponentsFromScene()</code>. Note this just gets from the active scene. If using a multi-scene setup then its recommended you get <code>MultiSceneElly</code> from <a href="https://github.com/JonathanMCarter/MultiScene">Multiscene</a>. The script works by getting all root objects in the scene and checking their children for the scrip tin question instead of going through each object individually.

For a single reference of just 1 object/script use the following. This uses the multiple get method but returns the first found. 

![SceneRef-single](https://user-images.githubusercontent.com/33253710/159187561-e8a5cbe6-3348-42ee-9722-9868a8d9b9cf.png)

For multiple, you can get a list of all found using the following. 

![SceneRef-collection](https://user-images.githubusercontent.com/33253710/159187654-63ed07a6-f0d0-4d08-951b-d0f425646157.png)
