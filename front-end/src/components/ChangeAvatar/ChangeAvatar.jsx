import AvatarEditor from 'react-avatar-editor';
import classes from './ChangeAvatar.module.css';
import { Modal, Text } from '@nextui-org/react';
import { Fragment, useRef, useState } from 'react';
import { Button, Slider, Upload } from 'antd';
import FetchApi from '../../apis/FetchApi';
import { IoImage } from 'react-icons/io5';
import toast from 'react-hot-toast';
import { UserApis } from '../../apis/ListApi';
import { useEffect } from 'react';

const ChangeAvatar = ({ userId, open, onSuccess }) => {
  const [isEdit, setIsEdit] = useState(false);
  const [file, setFile] = useState('');
  const [imageBase64, setImageBase64] = useState('');
  const [scaleImage, setScaleImage] = useState(0);
  const [isPreview, setIsPreview] = useState(false);
  const [waiting, setWaiting] = useState(false);
  const [fileList, setFileList] = useState([]);

  const refAvatar = useRef();
  const isHaveFile = Boolean(file !== '');

  const handleUpload = (info) => {
    const body = {
      image: refAvatar.current.getImageScaledToCanvas().toDataURL(),
    };

    setWaiting(true);
    const api =
      localStorage.getItem('role') === 'admin'
        ? UserApis.changeAvatarForAdmin
        : UserApis.changeAvatarForSro;
    toast.promise(FetchApi(api, body, null, [String(userId)]), {
      loading: 'Đang thay đổi ảnh',
      success: (res) => {
        setWaiting(false);
        onSuccess({ reload: true });
        return 'Thay đổi ảnh thành công';
      },
      error: (err) => {
        setWaiting(false);
        return 'Thay đổi ảnh thất bại';
      },
    });
  };

  useEffect(() => {
    setScaleImage(0);
  }, [open]);

  return (
    <Modal
      open={open}
      blur
      onClose={() => {
        onSuccess({ reload: false });
      }}
      closeButton
      preventClose
    >
      <Modal.Header>
        <Text p b size={14}>
          Thay đổi ảnh
        </Text>
      </Modal.Header>
      <Modal.Body>
        <div className={classes.avatarEdit}>
          <AvatarEditor
            ref={refAvatar}
            borderRadius={220}
            image={file}
            width={220}
            height={220}
            border={0}
            color={[0, 0, 0, 0.3]} // RGBA
            scale={(scaleImage + 100) / 100}
            rotate={0}
            className={classes.avatar}
          />
          <Slider
            style={{
              width: '220px',
            }}
            defaultValue={scaleImage}
            onChange={(value) => setScaleImage(value)}
            disabled={!isHaveFile}
          />
        </div>
        <Upload
          fileList={fileList}
          showUploadList={false}
          multiple={false}
          beforeUpload={(file) => {
            const isImage =
              file.type === 'image/jpeg' || file.type === 'image/png';
            if (!isImage) {
              toast.error(`${file.name} không phải là ảnh`);
            }
            return isImage || Upload.LIST_IGNORE;
          }}
          onChange={(info) => {
            setFile(info.file.originFileObj);
          }}
        >
          <Button auto color={'warning'} flat icon={<IoImage />}>
            Tải ảnh lên
          </Button>
        </Upload>
        <Button
          onClick={handleUpload}
          auto
          color={'default'}
          flat
          icon={<IoImage />}
        >
          Xác nhận
        </Button>
      </Modal.Body>
    </Modal>
  );
};

export default ChangeAvatar;
