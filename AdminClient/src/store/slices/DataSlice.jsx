import { createSlice } from "@reduxjs/toolkit";

const dataSlice = createSlice({
  name: "data",
  initialState: { client: "" },
  reducers: {
    setClient(state, action) {
      state.client = action.payload;
    },
  },
});

export const { setClient } = dataSlice.actions;
export default dataSlice.reducer;
