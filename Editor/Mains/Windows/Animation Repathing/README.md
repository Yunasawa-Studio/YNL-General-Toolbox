<img src="https://github.com/user-attachments/assets/3a633f70-57fa-4e40-b1b3-4a74c42b49d9"/>

<h1><div align="center"> YNL - GT: Animation Repathing </div></h1> 
<h4><div align="center"><i> Repath your invalid object paths in multiple Animation Clips. </i></div></h4>

<h2> ★ Contents </h2>

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

<h2> ★ Features & Instructions</h2>


<details><summary><b> (Free) Repath object paths in multiple Animation Clips </b></summary> 
    
- Firstly, select the object that has Animator component, drag it into `Referenced Animator` field; or you can simply select the Animator you want by clicking on the field.
- Then select Animation Clips you want to repath, they will appear on windows with all the paths.
- Now you put the invalid path into `Original Root` and the new valid on to `New Root`. Then click the button beside and the change will be applied.

<video src="https://github.com/user-attachments/assets/b13f98f0-4c3b-4508-b4a7-24503dc08186"></video>

- You can alse drag and drop the object you want to repath into the invalid path box, the old one will be replace with the new object path.

<video src="https://github.com/user-attachments/assets/33bdcd1b-0bdf-4535-99f9-e606d9558f04"></video>

- Or you just need to change the invalid path right away, press the apply button and see the result.
  
<video src="https://github.com/user-attachments/assets/6b40bdef-0693-4680-b39f-01e445101842"></video>

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
>
> <b>Be careful when trying to undo after you use automatic features, some of them may cause unexpected errors. (This will be fix soon)</b>

<br>

<details><summary><b> Destroying an object will be canceled if that object is animated. </b></summary>
<video src="https://github.com/user-attachments/assets/bb56b4dd-5774-43ae-a56d-13e7e6e41fb4"></video>
</details>

<details><summary><b> Destroying an object will be canceled if that object contains an animated object. </b></summary>
<video src="https://github.com/user-attachments/assets/47abba41-f5ca-4828-90f1-09b074a7b1e3"></video>
</details>

<details><summary><b> Destroying multiple objects will keep the animated objects and still destroy the unanimated ones. </b></summary>
<video src="https://github.com/user-attachments/assets/30bd0d05-2b6d-452e-8cd2-2fee8adc047f"></video>
</details>

<details><summary><b> Destroying an object that contains both animated and unanimated children will keep the animated and destroy the unanimated ones. </b></summary>
<video src="https://github.com/user-attachments/assets/9baadcac-25cd-4e88-bc34-94a4def02aaf"></video>
</details>

<br>

<details><summary><b> Renaming an animated object will automatically repath in clips. </b></summary>
<video src="https://github.com/user-attachments/assets/2f513ef2-dc40-437d-8524-de52c095e78e"></video>
</details>

<details><summary><b> Renaming an object contains animated objects will automatically repath in clips. </b></summary>
<video src="https://github.com/user-attachments/assets/3f165e81-c974-48c0-933a-c0d2e07c6a14"></video>
</details>

<details><summary><b> Renaming an object to a new name which is used by another object in the same path will be canceled. </b></summary>
<video src="https://github.com/user-attachments/assets/b934f4f3-e775-4ba5-bca5-22f53a12b1d9"></video>
</details>

<details><summary><b> Renaming multiple objects in a same path to a same name but one of them is animated will be canceled. </b></summary>
<video src="https://github.com/user-attachments/assets/ab7a5d84-b150-4d73-9eea-cd8314cd64f0"></video>
</details>

<details><summary><b> Renaming multiple objects in different paths to a same name will automatically repath in clips. </b></summary>
<video src="https://github.com/user-attachments/assets/1096539b-03da-4cfd-bccc-1f260ad410a9"></video>
</details>

<br>

<details><summary><b> Moving an animated object will automatically repath in clips. </b></summary>
<video src="https://github.com/user-attachments/assets/3553f500-6a25-4f13-8202-19996f2d2a61"></video>
</details>

<details><summary><b> Moving an object contains animated will automatically repath in clips. </b></summary>
<video src="https://github.com/user-attachments/assets/64b09302-20f6-4482-8d90-51c0816fc4a9"></video>
</details>

<details><summary><b> Moving an animated object out of Animator(s) scope will be canceled. </b></summary>
<video src="https://github.com/user-attachments/assets/304fdbbb-21a2-4e1f-89f9-91053ccbc08d"></video>
</details>

<details><summary><b> Moving an object contains animated objects out of Animator(s) scope will be canceled. </b></summary>
<video src="https://github.com/user-attachments/assets/8b588738-4546-4308-8482-8abb3da48a33"></video>
</details>

</details>

<details><summary><b> (Pro) Some additional extension features. </b></summary>
<br>

<table>
    <tr>
        <td>Toolbar Button to quickly enable/disable Automatic Mode.</td>
        <td><img src="https://github.com/user-attachments/assets/2a3fd7a6-19a1-4594-afe8-8a3b4c98b0a6"/></td>
    </tr>
    <tr>
        <td>Automatic Mode button, 3 functional buttons and some settings buttons.</td>
        <td><img align=center src="https://github.com/user-attachments/assets/36cab77a-16ab-44ad-8f6d-68fd07adcaf9"/></td>
    </tr>
    <tr>
        <td>Automatic action logs write the time and notify the events.</td>
        <td><img align=center src="https://github.com/user-attachments/assets/a24e3987-04f8-4fd8-947a-722cfff2669b"/></td>
    </tr>
</table>

</details>
