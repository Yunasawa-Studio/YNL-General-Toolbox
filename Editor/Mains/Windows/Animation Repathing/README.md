<img src="https://github.com/user-attachments/assets/3a633f70-57fa-4e40-b1b3-4a74c42b49d9"/>

<h1><div align="center"> YNL - GT: Animation Repathing </div></h1> 
<h4><div align="center"><i> Repath your invalid object paths in multiple Animation Clips. </i></div></h4>

<h2> ★ Contents </h2>

<!--

<table border="1">
    <tr>
        <th align="center">
            <img width="100" height="0"><p><small>Version</small></p>
        </th>
        <th align="center">
            <img width="1000" height="0"><p> <small>Features</small></p>
        </th>
    </tr>
    <tr>
        <td rowspan="7" align=center><b>Free</b></td>
        <td >✔️ Able to edit paths on multiple Animation Clips.</td>
    </tr>
    <tr>
        <td>
            <details><summary>✔️ Each Animation Clip has a random individual color and will show on every paths.</summary>
                <br>
                <img align=left width=30% src="https://github.com/user-attachments/assets/4dea5f03-98fe-4981-9580-6665db693e92"/>
                <img align=right width=57.5% src="https://github.com/user-attachments/assets/22a2c5d7-8a24-445b-a9d2-62b0ce118f62"/>
            </details>
        </td>
    </tr>
    <tr>
        <td>
            <details><summary>✔️ Can detect invalid paths appear in Animation Clips.</summary>
                <br>
                <img align=left width=95% src="https://github.com/user-attachments/assets/67b513d7-0fe7-4b4d-b829-0d26b222631c"/>
            </details>
        </td>
    </tr>
    <tr>
        <td>
            <details><summary>✔️ Repath an original object path with a new path.</summary>
                <br>
                <img align=left width=95% src="https://github.com/user-attachments/assets/a882ebb6-a840-4960-86e4-718d9ea0ce5f"/>
            </details>
        </td>
    </tr>
    <tr>
        <td>✔️ Drag and drop an object to object field to replace the old path with that object's path.</td>
    </tr>
    <tr>
        <td>❌ Replace a part of paths with a different word/name (Currently, only can replace a whole path with a new one).</td>
    </tr>
    <tr>
        <td>✔️ Support undo function</td>
    </tr>
    <tr>
        <td rowspan="7" align=center><b>Pro</b></td>
        <td>
            <details><summary>✔️ Easy to enable/disable each function of automatically mode.</summary>
                <br>
                <img align=center width=30% src="https://github.com/user-attachments/assets/0325712a-db71-4edd-8d81-03d6e991d6e0"/>
            </details>
        </td>
    </tr>
    <tr>
        <td>✔️ Toolbar button available to quickly turn on/off automatic mode.</td>
    </tr>
    <tr>
        <td>✔️ Automatically repath when destroy, rename or move objects.</td>
    </tr>
    <tr>
        <td>✔️ Automatically prevent invalid actions to avoid reference missing errors.</td>
    </tr>
    <tr>
        <td>
            <details><summary>✔️ Show dialog popup on any invalid action to notify about the probable errors.</summary>
                <br>
                <img align=center width=50% src="https://github.com/user-attachments/assets/8cb0462b-75d0-4355-8505-0e42d4da81c3"/>
            </details>
        </td>
    </tr>
    <tr>
        <td>
            <details><summary>✔️ Show logs with exact time and status of actions.</summary>
                <br>
                <img align=center width=100% src="https://github.com/user-attachments/assets/8de11101-83b8-450c-acd2-e118841e71a0"/>
            </details>
        </td>
    </tr>
    <tr>
        <td>✔️ Save all the settings and logs as User Settings so that they can still available on next Editor launch.</td>
    </tr>
</table>

-->

<h2> ★ Features & Instructions</h2>


<details><summary><b> (Free) Repath object paths in multiple Animation Clips </b></summary> 

- Firstly, select the object that has Animator component, drag it into `Referenced Animator` field; or you can simply select the Animator you want by clicking on the field.
- Then select Animation Clips you want to repath, they will appear on windows with all the paths.
- Now you put the invalid path into `Original Root` and the new valid on to `New Root`. Then click the button beside and the change will be applied.

<details><summary>Preview</summary><video src="https://github.com/user-attachments/assets/b13f98f0-4c3b-4508-b4a7-24503dc08186"></video></details>

- You can alse drag and drop the object you want to repath into the invalid path box, the old one will be replace with the new object path.
  
<details><summary>Preview</summary><video src="https://github.com/user-attachments/assets/33bdcd1b-0bdf-4535-99f9-e606d9558f04"></video></details>

- Or you just need to change the invalid path right away, press the apply button and see the result.
  
<details><summary>Preview</summary><video src="https://github.com/user-attachments/assets/6b40bdef-0693-4680-b39f-01e445101842"></video></details>

</details>


<details><summary><b> (Pro) Automatically repath when destroy, rename or move objects </b></summary>
<br>

> <b>This feature sometimes will not work good as expected an can cause some errors. If so, you should turn the tool of and do that manually.</b>
>
> If you have this error:
> ```
> MissingReferenceException: The object of type 'GameObject' has been destroyed but you are still trying to access it.
> Your script should either check if it is null or you should not destroy the object.
> ...
> ... (at Assets/Plugins/Yunasawa の Library/YのL - General Toolbox (Pro)/Editor/Mains/Windows/Animation Repathing/HandlerPro.cs:54)
> ... (at Assets/Plugins/Yunasawa の Library/YのL - Editor/Editor/Extensions/Handlers/HierarchyChangeCatcher.cs:99)
> ...
> ```
> then you should reload your scene or reenter prefab mode. If the error you have is not this, please tell me about it.

<details><summary><b> Automatically repath when renaming a single object. </b></summary>
<video src="https://github.com/user-attachments/assets/2f513ef2-dc40-437d-8524-de52c095e78e"></video>
</details>

<h3> Automatically repath when renaming a single object. </h3>



</details>


