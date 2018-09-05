import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../store/Rovers";

class Rover extends Component {
  componentWillMount() {
    const name = this.props.name;
    const earthDate = this.props.match.params.earthDate || "";
    this.props.requestRoverPhotos(name, earthDate);
  }

  componentWillReceiveProps(nextProps) {
    const name = nextProps.name;
    const earthDate = nextProps.match.params.earthDate || "";
    this.props.requestRoverPhotos(name, earthDate);
  }

  render() {
    return (
      <div>
        <h1>{this.props.name} Images</h1>
        <p>
          This component demonstrates fetching data from the server and working
          with URL parameters.
        </p>
        {renderPhotoTable(this.props)}
      </div>
    );
  }
}

function renderPhotoTable(props) {
  return (
    <table className="table">
      <thead>
        <tr>
          <th>Images</th>
        </tr>
      </thead>
      <tbody>
        {props.photos.map(photo => (
          <tr key={photo.id}>
            <td>
              <a href={photo.source} rel="_blank">
                {photo.id}
              </a>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default connect(
  state => state.rovers,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Rover);
