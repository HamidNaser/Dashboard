import { createSlice } from "@reduxjs/toolkit";

const resultSlice = createSlice({
  name: "result",
  initialState: { clientId: null, sessionId: null },

  reducers: {
    getResultQuery(state, action) {
      state.clientId = action.payload[0];
      state.sessionId = action.payload[1];
    }
  },
});

export const { getResultQuery } = resultSlice.actions;
export default resultSlice.reducer;
