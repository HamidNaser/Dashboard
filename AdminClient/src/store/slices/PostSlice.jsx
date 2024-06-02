import { createSlice } from "@reduxjs/toolkit";

const postSlice = createSlice({
  name: "postData",
  initialState: {
    locationsVLD: [],
    locationsCTR: [],
    locationsINST: [],
    providersVLD: [],
    providersCTR: [],
    providersINST: [],
    providersLocationsVLD: [],
    providersLocationsCTR: [],
    providersLocationsINST: [],
    providersLocationsBINST: [],
    providersSPEC: [],
  },
  reducers: {
    updatePostData(state, action) {
      const data = action.payload[0];
      const index = action.payload[1];

      const key = action.payload[2];

      if (!state[key]) {
        state[key] = [];
      }

      // state[key].push(data[key][index]);
      // state[key].sort((a, b) => a.clientId.localeCompare(b.clientId));

      if (data[key][index].trackingId) {
        state[key].push(data[key][index].trackingId);
        state[key].sort();
      } else {
        state[key].push(data[key][index].groupId);
      }
    },
    removePostData(state, action) {
      const data = action.payload[0];
      const myIndex = action.payload[1];
      const key = action.payload[2];

      // const clientId = data[key][myIndex].clientId;
      const clientId = data[key][myIndex].trackingId;

      // const index = state[key].findIndex((elem) => elem.clientId === clientId);

      if (clientId) {
        const index = state[key].findIndex((elem) => elem === clientId);
        state[key].splice(index, 1);
      } else {
        const groupId = data[key][myIndex].groupId;
        const index = state[key].findIndex((elem) => elem === groupId);
        state[key].splice(index, 1);
      }

      // if(state[key][myIndex])
      // if(state[key].includes(data[key][myIndex].trackingId)){
      //   state[key].splice(index, 1);
      // }

      // if (state[key].length === 0) {
      //   delete state[key];
      // }
    },

    updateAllPostData(state, action) {
      const data = action.payload[0];
      const key = action.payload[1];

      // state[key] = data[key];

      if(data[key][0].trackingId){
        state[key] = data[key].map((element) => element.trackingId);
      }else{
        state[key] = data[key].map((element) => element.groupId);
      }
    },

    deleteAllPostData(state, action) {
      const key = action.payload;
      // delete state[key];

      state[key] = [];
    },

    emptyPostState(state) {
      // return {};
      Object.keys(state).forEach((key) => {
        if (Array.isArray(state[key])) {
          state[key] = [];
        }
      });
    },
  },
});

export const {
  updatePostData,
  removePostData,
  updateAllPostData,
  deleteAllPostData,
  emptyPostState,
} = postSlice.actions;
export default postSlice.reducer;
