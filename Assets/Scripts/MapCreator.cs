using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreator : MonoBehaviour {
	public Camera cam;
	public GameObject map;
	public TMP_InputField fileName;

	private bool placingEntrance;
	private bool placingExit;
	private bool placedEntrance;
	private bool placedExit;
	private MapGenerator mg;

	void Start() {
		mg = map.GetComponent<MapGenerator>();
		placingEntrance = false;
		placingExit = false;
	}

	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			if(placingEntrance || placingExit) {
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
					Tile selected = hit.transform.gameObject.GetComponent<Tile>();
					if(!selected.IsAHill()) {
						for(int x = 0; x < mg.width; x++) {
							for(int y = 0; y < mg.height; y++) {
								if(mg.map[x, y] == selected) {
									if(x == 0 || y == 0 || x == mg.width-1 || y == mg.height-1) {
										GameObject current = null;
										if(placingEntrance) {
											if(placedEntrance) {
												current = mg.entrance;
											} else {
												current = Instantiate(mg.portalPrefab, Vector3.zero, Quaternion.identity);
												current.transform.SetParent(mg.transform);
											}
										} else {
											if(placedExit) {
												current = mg.exit;
											} else {
												current = Instantiate(mg.portalPrefab, Vector3.zero, Quaternion.identity);
												current.transform.SetParent(mg.transform);
											}
										}
										current.transform.localScale = new Vector3(mg.size, mg.size, 0.1f);
										int angle = FindAngle(hit.point);
										current.transform.eulerAngles = new Vector3(0, angle, 0);
										current.transform.position = FindPosition(angle, selected.transform.position);
										Portal portal = current.GetComponent<Portal>();
										if(placingEntrance) {
											portal.setSide(true);
											placingEntrance = false;
											if(!placedEntrance) {
												placedEntrance = true;
												mg.entrance = current;
											}
										} else {
											portal.setSide(false);
											placingExit = false;
											if(!placedExit) {
												placedExit = true;
												mg.exit = current;
											}
										}
									}
								}
							}
						}
					}
				}
			} else {
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
					Tile currentTile = hit.transform.GetComponent<Tile>();
					currentTile.SetIsAHill(!currentTile.IsAHill());
				}
			}
		}
	}

	private Vector3 FindPosition(int angle, Vector3 tilePos) {
		if(angle == 0) {
			return new Vector3(tilePos.x, mg.size, -(mg.size / 2));
		}
		if(angle == 90) {
			return new Vector3(-(mg.size/2), mg.size, tilePos.z);
		}
		if(angle == 180) {
			return new Vector3(tilePos.x, mg.size, tilePos.z + mg.size / 2);
		}
		if(angle == 270) {
			return new Vector3(tilePos.x + mg.size/2, mg.size, tilePos.z);
		}

		return Vector3.zero;
	}

	private int FindAngle(Vector3 point) {
		float distTop = Math.Abs(point.z - mg.topBound);
		float distLeft = Math.Abs(point.x - mg.leftBound);
		float distBottom = Math.Abs(point.z - mg.bottomBound);
		float distRight = Math.Abs(point.x - mg.rightBound);
		float bound = Math.Min(Math.Min(distTop, distLeft), Math.Min(distBottom, distRight));
		if(bound == distTop) {
			return 0;
		}
		if(bound == distLeft) {
			return 90;
		}
		if(bound == distBottom) {
			return 180;
		}

		return 270;
	}

	public void placeEntrance() {
		if(placingEntrance) {
			placingEntrance = false;
		} else if(placingExit) {
			placingExit = false;
			placingEntrance = true;
		} else {
			placingEntrance = true;
		}
	}

	public void placeExit() {
		if(placingExit) {
			placingExit = false;
		} else if(placingEntrance) {
			placingEntrance = false;
			placingExit = true;
		} else {
			placingExit = true;
		}
	}

	public void SaveLayout() {
		mg.navSurface.BuildNavMesh();
		SaveSystem.SaveMap(mg, fileName.text);
	}

	public void LoadLayout() {
		mg.ClearMap();
		MapLayoutData data = SaveSystem.LoadMap(fileName.text);
		mg.height = data.height;
		mg.width = data.width;
		mg.size = data.size;
		mg.InitializeMap();
		mg.InitializeHills(data.hills);
		mg.InitializePortals(data.entrance, data.exit);
		mg.navSurface.BuildNavMesh();
	}

	public void BackToMain() {
		SceneManager.LoadScene(0);
	}
}