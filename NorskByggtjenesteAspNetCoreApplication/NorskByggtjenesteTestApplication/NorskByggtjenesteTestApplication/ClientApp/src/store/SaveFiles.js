const requestSaveFilesType = 'REQUEST_SAVE_FILES';

const initialState = { files: [], currentContent:"" }

export const actionCreators = {
    requestSaveFiles: files => async (dispatch) => {    
  
      dispatch({ type: requestSaveFilesType, files });
  
      const url = `api/FileManagment/SaveFiles`;
      await fetch(url, {
        method: 'POST', 
        body: JSON.stringify(files),
        headers: {
          'Content-Type': 'application/json'
        }
      })
      .then(response => {
          if(response.ok){
            alert("All files have been successfully created");
          }
          else{
                alert("Something went wrong");
            }
        });
    }
  };

  export const reducer = (state, action) => {
    state = state || initialState;
  
    if (action.type === requestSaveFilesType) {
      return {
        ...state,
        files: action.files
      };
    }
    return state;
  };
  