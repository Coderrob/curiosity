const requestRoverPhotosType = "REQUEST_ROVER_PHOTOS";
const receiveRoverPhotosType = "RECEIVE_ROVER_PHOTOS";
const initialState = { date: '', photos: [], isLoading: false };

export const actionCreators = {
  requestRoverPhotos: (name, date) => async (dispatch, getState) => {
    if (
      date === getState().rovers.date &&
      name === getState().rovers.name
    ) {
      return;
    }

    dispatch({
      type: requestRoverPhotosType,
      name: name || "",
      date: date || ""
    });

    const url = date
      ? `api/rover/${name}/${date}`
      : `api/rover/${name}`;

    const response = await fetch(url);
    const photos = await response.json();

    dispatch({ type: receiveRoverPhotosType, name, date, photos });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestRoverPhotosType) {
    return {
      ...state,
      name: action.name,
      date: action.date,
      isLoading: true
    };
  }

  if (action.type === receiveRoverPhotosType) {
    return {
      ...state,
      name: action.name,
      date: action.date,
      photos: action.photos,
      isLoading: false
    };
  }

  return state;
};
