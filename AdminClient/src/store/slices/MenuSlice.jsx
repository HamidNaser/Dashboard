import { createSlice } from "@reduxjs/toolkit";

const menuSlice = createSlice({
  name: "menu",
  initialState: { clientIndex: null, toolsIndex: null, toggleMenu: true },
  reducers: {
    saveClientIndex(state, action) {
      state.clientIndex = action.payload;
    },
    saveToolsIndex(state, action) {
      state.toolsIndex = action.payload;
    },
    showDashboardData(state) {
      state.clientIndex = null;
      state.toolsIndex = null;
    },
    toggleMenu(state) {
      state.toggleMenu = !state.toggleMenu;
    },
    menuOn(state) {
      state.toggleMenu = true;
    },
    menuOff(state) {
      state.toggleMenu = false;
    },
  },
});

export const {
  saveClientIndex,
  saveToolsIndex,
  showDashboardData,
  toggleMenu,
  menuOn,
  menuOff,
} = menuSlice.actions;
export default menuSlice.reducer;
