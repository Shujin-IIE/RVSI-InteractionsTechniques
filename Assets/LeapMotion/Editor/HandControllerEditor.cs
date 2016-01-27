/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(HandController))]
public class HandControllerEditor : Editor {

  private const float BOX_RADIUS = 0.45f;
  private const float BOX_WIDTH = 0.965f;
  private const float BOX_DEPTH = 0.6671f;

  public void OnSceneGUI() {
    HandController Controller = (HandController)target;
    Vector3 origin = Controller.transform.TransformPoint(Vector3.zero);

    Vector3 local_top_left = new Vector3(-BOX_WIDTH, BOX_RADIUS, BOX_DEPTH);
    Vector3 top_left =
        Controller.transform.TransformPoint(BOX_RADIUS * local_top_left.normalized);

    Vector3 local_top_right = new Vector3(BOX_WIDTH, BOX_RADIUS, BOX_DEPTH);
    Vector3 top_right =
        Controller.transform.TransformPoint(BOX_RADIUS * local_top_right.normalized);

    Vector3 local_bottom_left = new Vector3(-BOX_WIDTH, BOX_RADIUS, -BOX_DEPTH);
    Vector3 bottom_left =
        Controller.transform.TransformPoint(BOX_RADIUS * local_bottom_left.normalized);

    Vector3 local_bottom_right = new Vector3(BOX_WIDTH, BOX_RADIUS, -BOX_DEPTH);
    Vector3 bottom_right =
        Controller.transform.TransformPoint(BOX_RADIUS * local_bottom_right.normalized);

    Handles.DrawLine(origin, top_left);
    Handles.DrawLine(origin, top_right);
    Handles.DrawLine(origin, bottom_left);
    Handles.DrawLine(origin, bottom_right);

    Vector3 top_normal = Controller.transform.TransformDirection(
        Vector3.Cross(local_top_left, local_top_right));
    float top_angle = Vector3.Angle(local_top_left, local_top_right);
    Handles.DrawWireArc(origin, top_normal,
                        Controller.transform.TransformDirection(local_top_left),
                        top_angle, Controller.transform.lossyScale.x * BOX_RADIUS);

    Vector3 left_normal = Controller.transform.TransformDirection(
        Vector3.Cross(local_bottom_left, local_top_left));
    float left_angle = Vector3.Angle(local_bottom_left, local_top_left);
    Handles.DrawWireArc(origin, left_normal,
                        Controller.transform.TransformDirection(local_bottom_left),
                        left_angle, Controller.transform.lossyScale.x * BOX_RADIUS);

    Vector3 bottom_normal = Controller.transform.TransformDirection(
        Vector3.Cross(local_bottom_left, local_bottom_right));
    float bottom_angle = Vector3.Angle(local_bottom_left, local_bottom_right);
    Handles.DrawWireArc(origin, bottom_normal,
                        Controller.transform.TransformDirection(local_bottom_left),
                        bottom_angle, Controller.transform.lossyScale.x * BOX_RADIUS);

    Vector3 right_normal = Controller.transform.TransformDirection(
        Vector3.Cross(local_bottom_right, local_top_right));
    float right_angle = Vector3.Angle(local_bottom_right, local_top_right);
    Handles.DrawWireArc(origin, right_normal,
                        Controller.transform.TransformDirection(local_bottom_right),
                        right_angle, Controller.transform.lossyScale.x * BOX_RADIUS);

    Vector3 local_left_face = Vector3.Lerp(local_top_left, local_bottom_left, 0.5f);
    Vector3 local_right_face = Vector3.Lerp(local_top_right, local_bottom_right, 0.5f);

    Vector3 across_normal = Controller.transform.TransformDirection(-Vector3.forward);
    float across_angle = Vector3.Angle(local_left_face, local_right_face);
    Handles.DrawWireArc(origin, across_normal,
                        Controller.transform.TransformDirection(local_left_face),
                        across_angle, Controller.transform.lossyScale.x * BOX_RADIUS);

    Vector3 local_top_face = Vector3.Lerp(local_top_left, local_top_right, 0.5f);
    Vector3 local_bottom_face = Vector3.Lerp(local_bottom_left, local_bottom_right, 0.5f);

    Vector3 depth_normal = Controller.transform.TransformDirection(-Vector3.right);
    float depth_angle = Vector3.Angle(local_top_face, local_bottom_face);
    Handles.DrawWireArc(origin, depth_normal,
                        Controller.transform.TransformDirection(local_top_face),
                        depth_angle, Controller.transform.lossyScale.x * BOX_RADIUS);
  }

  public override void OnInspectorGUI() {
    HandController Controller = (HandController)target;

    Controller.separateLeftRight = EditorGUILayout.Toggle("Separate Left/Right",
                                                          Controller.separateLeftRight);

    if (Controller.separateLeftRight) {
      Controller.leftGraphicsModel =
          (HandModel)EditorGUILayout.ObjectField("Left Hand Graphics Model",
                                                 Controller.leftGraphicsModel,
                                                 typeof(HandModel), true);
      Controller.rightGraphicsModel =
          (HandModel)EditorGUILayout.ObjectField("Right Hand Graphics Model",
                                                 Controller.rightGraphicsModel,
                                                 typeof(HandModel), true);
      Controller.leftPhysicsModel =
          (HandModel)EditorGUILayout.ObjectField("Left Hand Physics Model",
                                                 Controller.leftPhysicsModel,
                                                 typeof(HandModel), true);
      Controller.rightPhysicsModel =
          (HandModel)EditorGUILayout.ObjectField("Right Hand Physics Model",
                                                 Controller.rightPhysicsModel,
                                                 typeof(HandModel), true);
    }
    else {
      Controller.leftGraphicsModel = Controller.rightGraphicsModel = 
          (HandModel)EditorGUILayout.ObjectField("Hand Graphics Model",
                                                 Controller.leftGraphicsModel,
                                                 typeof(HandModel), true);

      Controller.leftPhysicsModel = Controller.rightPhysicsModel = 
          (HandModel)EditorGUILayout.ObjectField("Hand Physics Model",
                                                 Controller.leftPhysicsModel,
                                                 typeof(HandModel), true);
    }

    Controller.toolModel = 
        (ToolModel)EditorGUILayout.ObjectField("Tool Model",
                                               Controller.toolModel,
                                               typeof(ToolModel), true);

    EditorGUILayout.Space();

    Controller.isHeadMounted = EditorGUILayout.Toggle("Is Head Mounted",
                                                      Controller.isHeadMounted);

    Controller.mirrorZAxis = EditorGUILayout.Toggle("Mirror Z Axis", Controller.mirrorZAxis);

    Controller.handMovementScale =
        EditorGUILayout.Vector3Field("Hand Movement Scale", Controller.handMovementScale);

    Controller.destroyHands = EditorGUILayout.Toggle("Destroy Hands",
                                                      Controller.destroyHands);

    EditorGUILayout.Space();
    Controller.enableRecordPlayback = EditorGUILayout.Toggle("Enable Record/Playback",
                                                             Controller.enableRecordPlayback);
    if (Controller.enableRecordPlayback) {
      Controller.recordingAsset =
          (TextAsset)EditorGUILayout.ObjectField("Recording File",
                                                 Controller.recordingAsset,
                                                 typeof(TextAsset), true);
      Controller.recorderSpeed = EditorGUILayout.FloatField("Playback Speed Multiplier",
                                                             Controller.recorderSpeed);
      Controller.recorderLoop = EditorGUILayout.Toggle("Playback Loop",
                                                        Controller.recorderLoop);
    }
        
    if (GUI.changed)
      EditorUtility.SetDirty(Controller);

    Undo.RecordObject(Controller, "Hand Preferences Changed: " + Controller.name);
  }
}
