import { createSlice } from "@reduxjs/toolkit";

const asideSlice = createSlice({
  name: "aside",
  initialState: { activeDeputy: null, activeStatus: null },
  reducers: {
    setActiveDeputy(state, action) {
      state.activeDeputy = action.payload;
    },
    setActiveStatus(state, action) {
      state.activeStatus = action.payload;
    }
  },
});

export const { setActiveDeputy, setActiveStatus } = asideSlice.actions;

export default asideSlice.reducer;
