const requestRoverPhotosType = "REQUEST_ROVER_PHOTOS";
const receiveRoverPhotosType = "RECEIVE_ROVER_PHOTOS";
const initialState = { earthDate: "", photos: [], isLoading: false };

export const actionCreators = {
  requestRoverPhotos: (name, earthDate) => async (dispatch, getState) => {
    if (
      earthDate === getState().rovers.earthDate &&
      name === getState().rovers.name
    ) {
      return;
    }

    dispatch({
      type: requestRoverPhotosType,
      name: name || "",
      earthDate: earthDate || ""
    });

    const url = earthDate
      ? `api/rover/${name}/${earthDate}`
      : `api/rover/${name}`;

    const response = await fetch(url);
    const photos = await response.json();

    dispatch({ type: receiveRoverPhotosType, name, earthDate, photos });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestRoverPhotosType) {
    return {
      ...state,
      name: action.name,
      earthDate: action.earthDate,
      isLoading: true
    };
  }

  if (action.type === receiveRoverPhotosType) {
    return {
      ...state,
      name: action.name,
      earthDate: action.earthDate,
      photos: action.photos,
      isLoading: false
    };
  }

  return state;
};
